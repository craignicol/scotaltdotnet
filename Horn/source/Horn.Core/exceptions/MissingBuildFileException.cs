using System;

namespace Horn.Core
{
    [global::System.Serializable]
    public class MissingBuildFileException : Exception
    {

        public MissingBuildFileException() { }

        public MissingBuildFileException(string message) : base(message) { }
        
        public MissingBuildFileException(string message, Exception inner) : base(message, inner) { }
        
        protected MissingBuildFileException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}