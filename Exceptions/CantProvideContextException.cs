using System;

namespace Services.SequenceProcessor.Exceptions
{
    public class CantProvideContextException : Exception
    {
        public Type Type { get; }

        public CantProvideContextException(string message)
            : base(message)
        {
        }
        
        public CantProvideContextException(string message, Type type)
            : base(message)
        {
            Type = type;
        }

        public CantProvideContextException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        public CantProvideContextException(string message, Type type, Exception innerException)
            : base(message, innerException)
        {
            Type = type;
        }
    }
}