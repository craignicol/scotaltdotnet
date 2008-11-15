using System.Collections.Generic;

namespace Horn.Core.PackageCommands
{
    public interface IPackageCommand
    {
        void Execute(IDictionary<string, IList<string>> switches);
    }
}