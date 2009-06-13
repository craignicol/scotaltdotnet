using System;

namespace Horn.Core
{
    [global::System.Serializable]
    public class UnkownInstallPackageException : Exception
    {
        public UnkownInstallPackageException() { }

        public UnkownInstallPackageException(string message) : base(message) { }
        
        public UnkownInstallPackageException(string message, Exception inner) : base(message, inner) { }
        
        protected UnkownInstallPackageException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }        
    }
}