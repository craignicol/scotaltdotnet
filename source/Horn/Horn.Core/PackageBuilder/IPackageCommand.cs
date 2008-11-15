using System.Collections.Generic;

namespace Horn.Core.Package
{
    public interface IPackageCommand
    {
        void Execute(IDictionary<string, IList<string>> switches);
    }
}