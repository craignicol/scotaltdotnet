namespace Horn.Core.Spec.Unit.CmdLine
{
    using System.Collections.Generic;
    using System.IO;
    using Utils.CmdLine;
    using Xunit;

    public abstract class CmdLineSpecificationBase : DirectoryStructureSpecificationBase
    {
        private TextWriter textWriter;
        protected SwitchParser parser;

        protected IDictionary<string, IList<string>> ParsedArgs { get; set; }

        protected TextWriter Output { get { return textWriter; } }

        protected bool IsValid { get; set; }

        protected override void Before_each_spec()
        {
            base.Before_each_spec();

            textWriter = new StringWriter();
        }
    }


    public abstract class CmdLineErrorSpecificationBase : CmdLineSpecificationBase
    {
        protected abstract string Args { get; }
        protected abstract string ExpectErrorMessage { get; }

        protected override void Because()
        {
            parser = new SwitchParser(Output);
            ParsedArgs = parser.Parse(new[] { Args });
            IsValid = parser.IsValid(ParsedArgs);
        }
    }
}