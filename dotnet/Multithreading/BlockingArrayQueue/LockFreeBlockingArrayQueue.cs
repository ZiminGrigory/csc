using System;
using System.Threading;

namespace BlockingArrayQueue
{
    public class LockFreeBlockingArrayQueue<T> : IBlockingArrayQueue<T>
    {
        private readonly int _mFixedSize;
        private Fields _fields;
        private class Fields
        {
            public int Head { get; set; }
            public int Tail { get; set; }
            public int Size { get; set; }
            public T[] Buffer { get; set; }
        }

        public LockFreeBlockingArrayQueue(int fixedQueueSize)
        {
            if (fixedQueueSize <= 0)
            {
                throw new ArgumentException("fixedQueueSize should be greater than 0");
            }

            _mFixedSize = fixedQueueSize;
            _fields = new Fields
            {
                Size = 0,
                Tail = 0,
                Head = 0,
                Buffer = new T[_mFixedSize]
        };
        }

        public void Enqueue(T value)
        {
            var nFields = new Fields();
            nFields.Buffer = new T[_mFixedSize];
            if (_fields.Size == 0)
            {
                nFields.Size = 1;
                nFields.Tail = 0;
                nFields.Head = 0;
            }
            else
            {
                nFields.Size = _fields.Size + 1;
                nFields.Tail = (_fields.Tail + 1) % _mFixedSize;
                nFields.Head = _fields.Head;

                Array.Copy(_fields.Buffer, nFields.Buffer, _mFixedSize);
            }

            nFields.Buffer[nFields.Tail] = value;

            while (_fields != Interlocked.CompareExchange(ref _fields, nFields, _fields))
            {
                nFields = new Fields();
                _fields.Buffer = new T[_mFixedSize];
                if (_fields.Size == 0)
                {
                    nFields.Size = 1;
                    nFields.Tail = 0;
                    nFields.Head = 0;
                }
                else
                {
                    nFields.Size = _fields.Size + 1;
                    nFields.Tail = (_fields.Tail + 1) % _mFixedSize;
                    nFields.Head = _fields.Head;

                    Array.Copy(_fields.Buffer, nFields.Buffer, _mFixedSize);
                }
                nFields.Buffer[nFields.Tail] = value;
            }

        }

        public T Dequeue()
        {
            T result = _fields.Buffer[_fields.Head];
            var nFields = new Fields
            {
                Size = _fields.Size - 1,
                Tail = _fields.Tail,
                Head = (_fields.Head + 1) % _mFixedSize,
                Buffer = _fields.Buffer
            };

            while (_fields != Interlocked.CompareExchange(ref _fields, nFields, _fields))
            {
                result = _fields.Buffer[_fields.Head];
                nFields = new Fields
                {
                    Size = _fields.Size - 1,
                    Tail = _fields.Tail,
                    Head = (_fields.Head + 1) % _mFixedSize,
                    Buffer = _fields.Buffer
                };
            }

            return result;
        }

        public bool TryEnqueue(T value)
        {
            if (_fields.Size >= _mFixedSize)
            {
                return false;
            }

            Enqueue(value);
            return true;
        }

        public bool TryDequeue(out T value)
        {
            if (_fields.Size > 0)
            {
                value = Dequeue();
                return true;
            }

            value = default(T);
            return false;
        }

        public void Clear()
        {
            var nFields = new Fields
            {
                Size = 0,
                Tail = 0,
                Head = 0,
                Buffer = _fields.Buffer
            };

            while (_fields != Interlocked.CompareExchange(ref _fields, nFields, _fields))
            {
                nFields = new Fields
                {
                    Size = 0,
                    Tail = 0,
                    Head = 0,
                    Buffer = _fields.Buffer
                };
            }
        }

        // in some moment it was valid, there is no need for locking
        int IBlockingArrayQueue<T>.Size()
        {
            return _fields.Size;
        }
    }
}
