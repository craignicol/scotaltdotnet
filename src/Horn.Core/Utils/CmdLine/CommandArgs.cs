using System;
using System.Collections.Generic;

namespace Horn.Core.Utils.CmdLine
{
    public class CommandArgs : ICommandArgs
    {
        public bool RebuildOnly { get; private set; }

        public string InstallName { get; private set; }

        public CommandArgs(IDictionary<string, IList<string>> switches)
        {
            InstallName = switches["install"][0];

            RebuildOnly = switches.Keys.Contains("rebuildonly");
        }
    }
}