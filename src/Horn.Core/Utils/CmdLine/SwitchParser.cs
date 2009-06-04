using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Horn.Core.PackageStructure;

namespace Horn.Core.Utils.CmdLine
{
    using System.IO;

    public class SwitchParser
    {

        private readonly TextWriter output;
        private readonly Parameter[] paramTable;

        public const string HELP_TEXT =
@"HORN - SCOTALT.NET
http://code.google.com/p/scotaltdotnet/
http://groups.google.com/group/scotaltnet
Usage : horn -install:<component>
Options :
    -rebuildonly         Do not check for the latest source code.";

        public bool IsValid(IDictionary<string, IList<string>> commandLineArgs)
        {
            var ret = true;

            foreach (var paramRow in paramTable)
            {
                var arg = commandLineArgs.ContainsKey(paramRow.Key) ? commandLineArgs[paramRow.Key] : null;

                if (arg == null)
                {
                    if (paramRow.Required)
                        ret = OutputValidationMessage(string.Format("Missing required argument key: {0}.", paramRow.Key));

                    continue;
                }

                if ((paramRow.RequiresArgument) && (arg.Count == 0 || string.IsNullOrEmpty(arg[0])))
                    return OutputValidationMessage(string.Format("Missing argument value for key: {0}.", paramRow.Key));

                if (arg.Count > 1 && !paramRow.Reoccurs)
                    ret = OutputValidationMessage(string.Format("Argument key cannot reoccur: {0}.", paramRow.Key));

                foreach (var value in arg)
                {
                    if (paramRow.Values != null &&
                        paramRow.Values.Length != 0 &&
                        !SwitchValueIsValid(arg))
                        ret = OutputValidationMessage(string.Format("Argument value for key {0} is invalid: {1}.", paramRow.Key, value));
                }
            }

            foreach (var keyValuePair in commandLineArgs)
            {
                var paramRow = Array.Find(paramTable, match => match.Key == keyValuePair.Key);

                if (paramRow == null)
                    ret= OutputValidationMessage(string.Format("Argument key unknown: {0}.", keyValuePair.Key));
            }

            return ret;
        }

        public Dictionary<string, IList<string>> Parse(string[] args)
        {
            const string ARGS_REGEX = @"-([a-zA-Z_][a-zA-Z_0-9]{0,}):?((?<=:).{0,})?";
            string name;
            Match match;

            var parsedArgs = new Dictionary<string, IList<string>>();

            if ((args == null) || (args.Length == 0) || ((args[0].ToLower().Equals("-help"))))
            {
                output.WriteLine(HELP_TEXT);

                return new HelpReturnValue();
            }

            foreach (string arg in args)
            {
                match = Regex.Match(arg, ARGS_REGEX, RegexOptions.IgnorePatternWhitespace);

                if ((match == null) || (!match.Success) || (match.Groups.Count != 3))
                    continue;

                name = match.Groups[1].Value;
                string value = match.Groups[2].Value;

                name = name.ToLower();

                if (!parsedArgs.ContainsKey(name))
                    parsedArgs.Add(name, new List<string>());

                if (!parsedArgs[name].Contains(value))
                    parsedArgs[name].Add(value);
            }

            return parsedArgs;
        }



        private bool SwitchValueIsValid(IList<string> arg)
        {
            return paramTable
                       .Where(param => param.Values
                                           .Where(value => arg.Contains(value)
                                           ).Count() > 0)
                       .Count() > 0;
        }

        private bool OutputValidationMessage(string message)
        {
            output.WriteLine(message);

            string usage = "USAGE:" + Environment.NewLine;

            foreach (var paramRow in paramTable)
                usage += string.Format("{0}-{1}:{{{2}}}{3}{4}" + Environment.NewLine, paramRow.Required ? string.Empty : "[", paramRow.Key, paramRow.Values != null ? string.Join(" | ", new List<string>(paramRow.Values).ToArray()) : "?", paramRow.Reoccurs ? "*" : "", paramRow.Required ? "" : "]");

            output.WriteLine();
            output.WriteLine(usage);

            return false;
        }



        public SwitchParser(TextWriter output, IPackageTree root)
        {
            this.output = output;

            var parameters = new List<Parameter>();

            root.BuildNodes().ForEach(c => parameters.Add(new Parameter("install", true, new[] {c.Name}, false)));

            parameters.Add(new Parameter("rebuildonly", false, new string[]{}, false));

            paramTable = parameters.ToArray();
        }


    }
}