using System.Collections.Generic;
using System.IO;
using Horn.Core.Utils.CmdLine;

namespace Horn.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = new StringWriter();

            var parser = new SwitchParser(output);

            var parsedArgs = parser.Parse(args);

            if (!IsAValidRequest(parser, parsedArgs))
            {
                System.Console.WriteLine(output.ToString());
                return;
            }

            //continue
        }
        
        private static bool IsAValidRequest(SwitchParser parser, IDictionary<string, IList<string>> parsedArgs)
        {
            if (IsHelpTextSwitch(parsedArgs))
                return false;

            return parser.IsValid(parsedArgs);
        }

        private static bool IsHelpTextSwitch(IDictionary<string, IList<string>> parsedArgs)
        {
            return parsedArgs != null && parsedArgs is HelpReturnValue;
        }
    }
}