using System.Collections.Generic;
using Horn.Domain.PackageStructure;

namespace Horn.Core.PackageCommands
{
    public interface IPackageCommand
    {
        void Execute(IPackageTree packageTree, IDictionary<string, IList<string>> switches);
    }
}