using System;

namespace Horn.Core
{
    [global::System.Serializable]
    public class UnknownBuildComponentException : Exception
    {

        public UnknownBuildComponentException() { }

        public UnknownBuildComponentException(string message) : base(message) { }
        
        public UnknownBuildComponentException(string message, Exception inner) : base(message, inner) { }
        
        protected UnknownBuildComponentException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}