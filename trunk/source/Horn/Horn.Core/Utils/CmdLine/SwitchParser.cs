using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Horn.Core.Utils.CmdLine
{
    using System.IO;

    public class SwitchParser
    {
        #region console help text

        public const string HELP_TEXT = 
@"Horn
                                          
http://code.google.com/p/scotaltdotnet/

Usage : horn -install:<component>
Options :
    <none>";

        #endregion


        private readonly TextWriter output;
        private readonly Parameter[] paramTable;

        public IDictionary<string, IList<string>> Parse(string[] args)
        {
            const string ARGS_REGEX = @"-([a-zA-Z_][a-zA-Z_0-9]{0,}):(.{0,})";
            string name;
            Match match;

            var parsedArgs = new Dictionary<string, IList<string>>();

            if (args == null)
                return parsedArgs;

            if((args.Length > 0) && (args[0].ToLower().Equals("-help")))
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

                if (arg.Count == 0)
                    return OutputValidationMessage(string.Format("Missing argument value for key: {0}.", paramRow.Key));

                if (arg.Count > 1 && !paramRow.Reoccurs)
                    ret = OutputValidationMessage(string.Format("Argument key cannot reoccur: {0}.", paramRow.Key));

                foreach (string value in arg)
                {
                    if (paramRow.Values != null &&
                        paramRow.Values.Length != 0 &&
                        Array.Find(paramRow.Values, match => match == value) == null)
                        ret = OutputValidationMessage(string.Format("Argument {0} has already been given the value: {1}.", paramRow.Key, value));
                }
            }

            foreach (KeyValuePair<string, IList<string>> keyValuePair in commandLineArgs)
            {
                var paramRow = Array.Find(paramTable, match => match.Key == keyValuePair.Key);

                if (paramRow == null)
                    ret= OutputValidationMessage(string.Format("Argument key unknown: {0}.", keyValuePair.Key));
            }

            return ret;
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

        public SwitchParser(TextWriter output)
        {
            this.output = output;

            //TODO: Parse from file system, tree?
            paramTable = new[]
                             {
                                 new Parameter("install", true, new[] { "horn" }, false)
                             };
        }
    }
}