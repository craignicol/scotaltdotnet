namespace Horn.Core.Utils.CmdLine
{
    using System;
    using Boo.Lang.Useful.CommandLine;

    [Serializable]
    public class CommandLineArguments : AbstractCommandLine
    {
        [Option(LongForm = "install", ShortForm = "i")]
        public string PackageName;

        // BUG: Not working the same as previous switch parser
        [Option(LongForm = "help", ShortForm = "?")]
        public bool ShowHelp;

        [Option(LongForm = "path", ShortForm = "p")]
        public string Path;
    }
}