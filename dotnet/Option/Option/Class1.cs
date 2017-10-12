using System;

namespace OptionHW
{
    public class Option<T>
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
                    throw new NullReferenceException("Trying get value from 'Option.None' is ambiguous");
                }

                return _value;
            }
        }

        public Option<T2> Map<T2>(Func<T, T2> f)
        {
            return IsNone ? new Option<T2>(f(_value)) : new Option<T2>();
        }

        public static Option<T> Flatten(Option<Option<T>> option)
        {
            return option.IsSome ? option.Value : new Option<T>();
        }
    }
}
