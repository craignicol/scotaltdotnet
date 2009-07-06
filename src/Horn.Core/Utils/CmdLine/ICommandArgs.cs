namespace Horn.Core.Utils.CmdLine
{
    public interface ICommandArgs
    {
        bool RebuildOnly { get; }
        string InstallName { get; }
    }
}