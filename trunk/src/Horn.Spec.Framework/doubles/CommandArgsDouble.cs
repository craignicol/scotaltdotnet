using Horn.Core.Utils.CmdLine;

namespace Horn.Spec.Framework.doubles
{
    public class CommandArgsDouble : ICommandArgs
    {
        public string InstallName{ get; private set; }

        public bool RebuildOnly { get; private set; }

        public CommandArgsDouble(string installName)
        {
            InstallName = installName;
        }

        public CommandArgsDouble(string installName, bool rebuildOnly) : this(installName)
        {
            InstallName = installName;
            RebuildOnly = rebuildOnly;
        }
    }
}