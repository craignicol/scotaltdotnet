using System.Collections.Generic;
using Horn.Core.PackageStructure;

namespace Horn.Core.PackageCommands
{
    using Utils.CmdLine;

    public interface IPackageCommand
    {
        void Execute(IPackageTree packageTree, CommandLineArguments switches);
    }
}