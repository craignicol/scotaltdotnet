using Horn.Core.Utils.CmdLine;

namespace Horn.Domain.Spec.Unit.CmdLine
{
    public class When_horn_receives_a_refresh_command : CmdLineSpecificationBase
    {
        private const string arg = "-refresh";

        protected override void Because()
        {
            parser = new SwitchParser(Output, packageTree);

            ParsedArgs = parser.Parse(new[] { arg });
            IsValid = parser.IsValid(ParsedArgs);            
        }
    }
}