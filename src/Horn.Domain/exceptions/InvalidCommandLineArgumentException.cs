using System;

namespace Horn.Domain.Exceptions
{
    [global::System.Serializable]
    public class InvalidCommandLineArgumentException : Exception
    {

        public InvalidCommandLineArgumentException() { }

        public InvalidCommandLineArgumentException(string message) : base(message) { }
        
        public InvalidCommandLineArgumentException(string message, Exception inner) : base(message, inner) { }
        
        protected InvalidCommandLineArgumentException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}