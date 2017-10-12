using System;
using System.Collections.Generic;

namespace OptionHW
{
    public sealed class Option<T>
    {
        private readonly T _value;

        public bool IsSome { get; }

        public bool IsNone => !IsSome;

        public static Option<T> Some(T value)
        {
            return new Option<T>(value);
        }

        private Option(T value)
        {
            IsSome = true;
            _value = value;
        }

        public static Option<T> None()
        {
            return new Option<T>();
        }

        private Option()
        {
            IsSome = false;
        }

        public T Value
        {
            get
            {
                if (IsNone)
                {
                    throw new NullReferenceException();
                }

                return _value;
            }
        }

        public Option<T2> Map<T2>(Func<T, T2> f)
        {
            return IsSome ? new Option<T2>(f(_value)) : new Option<T2>();
        }

        public static Option<T> Flatten(Option<Option<T>> option)
        {
            return option.IsSome ? option.Value : new Option<T>();
        }

        public override bool Equals(object obj)
        {
            return obj is Option<T> option && EqualityComparer<T>.Default.Equals(_value, option._value);
        }
        
        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(_value);
        }
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value)
        {
            return Option<T>.Some(value);
        }
    }
}
