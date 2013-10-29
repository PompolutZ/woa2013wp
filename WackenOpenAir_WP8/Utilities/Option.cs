using System;

namespace WackenOpenAir.Utilities
{
    public class OptionException : Exception
    {
        private const string OptionExceptionMessage = "Option None doesn't contains any value.";

        public OptionException()
        {    
        }

        public OptionException(string message, Exception innerException)
            : base(OptionExceptionMessage, innerException)
        {
        }

        public OptionException(string message)
            : base(OptionExceptionMessage)
        {
        }
    }

    public struct Option<T>
    {
        private readonly T _value;
        private readonly bool _some;

        private Option(T value)
        {
            _value = value;
            _some = true;
        }

        public static Option<T> None()
        {
            var option = new Option<T>();
            return option;
        }

        public static Option<T> Some(T value)
        {
            var option = new Option<T>(value);
            return option;
        }

        public bool IsSome 
        {
            get { return _some; }
        }

        public T Value 
        {
            get
            {
                if (!_some)
                    throw new OptionException();

                return _value;
            }
        }
    }
}