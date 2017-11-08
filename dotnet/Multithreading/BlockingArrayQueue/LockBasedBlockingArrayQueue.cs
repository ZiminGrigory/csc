using System;
using System.Threading;

namespace BlockingArrayQueue
{
    public class LockBasedBlockingArrayQueue<T> : IBlockingArrayQueue<T>
    {
        private readonly int _mFixedSize;
        private int Head { get; set; }
        private int Tail { get; set; }
        private int Size { get; set; }
        private readonly T[] _buffer;

        private readonly object _mLock = new object();

        public LockBasedBlockingArrayQueue(int fixedQueueSize)
        {
            if (fixedQueueSize <= 0)
            {
                throw new ArgumentException("fixedQueueSize should be greater than 0");
            }

            _mFixedSize = fixedQueueSize;
            _buffer = new T[_mFixedSize];
            Head = 0;
            Tail = 0;
            Size = 0;
        }

        public void Enqueue(T value)
        {
            lock (_mLock)
            {
                while (Size == _mFixedSize)
                {
                    Monitor.Wait(_mLock);
                }

                HandleEnqueue(value);
            }
        }

        private void HandleEnqueue(T value)
        {
            if (Size == 0)
            {
                Head = 0;
                Tail = 0;
            }
            else
            {
               Tail = (Head + 1) % _mFixedSize;
            }

            _buffer[Tail] = value;
            ++Size;

            Monitor.PulseAll(_mLock);
        }

        public T Dequeue()
        {
            lock (_mLock)
            {
                while (Size == 0)
                {
                    Monitor.Wait(_mLock);
                }

                return DequeueValue();
            }
        }

        private T DequeueValue()
        {
            T result = _buffer[Head];
            Head = (Head + 1) % _mFixedSize;
            --Size;
            return result;
        }

        public bool TryEnqueue(T value)
        {
            lock (_mLock)
            {
                if (Size >= _mFixedSize)
                {
                    return false;
                }

                HandleEnqueue(value);
                return true;
            }
        }

        public bool TryDequeue(out T value)
        {
            lock (_mLock)
            {
                if (Size > 0)
                {
                    value = DequeueValue();
                    return true;
                }

                value = default(T);
                return false;
            }
        }

        public void Clear()
        {
            lock (_mLock)
            {
                Size = 0;
                Tail = 0;
                Head = 0;
            }
        }

        // in some moment it was valid, there is no need for locking
        int IBlockingArrayQueue<T>.Size()
        {
            return Size;
        }
    }
}
