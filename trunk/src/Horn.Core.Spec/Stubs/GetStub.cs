using System;
using Horn.Core.GetOperations;
using Horn.Core.PackageStructure;
using Horn.Core.SCM;

namespace Horn.Core.Spec.Doubles
{
    public class GetStub : IGet
    {
        private SourceControl sourceControl;

        public IGet Package(Package packageToGet)
        {
            throw new NotImplementedException();
        }

        public IGet From(SourceControl sourceControlToGetFrom)
        {
            sourceControl = sourceControlToGetFrom;

            return this;
        }

        public IPackageTree ExportTo(IPackageTree packageTree)
        {
            throw new NotImplementedException();
        }

        public IPackageTree ExportTo(IPackageTree packageTree, string path, bool initialise)
        {
            throw new NotImplementedException();
        }
    }
}