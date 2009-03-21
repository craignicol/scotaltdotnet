using Horn.Core.PackageStructure;

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
        protected PackageTree packageTree;

        protected IDictionary<string, IList<string>> ParsedArgs { get; set; }

        protected TextWriter Output { get { return textWriter; } }

        protected bool IsValid { get; set; }

        protected override void Before_each_spec()
        {
            base.Before_each_spec();

            textWriter = new StringWriter();

            packageTree = new PackageTree(new DirectoryInfo(root), null, new BuildFileExtensionResolver());
        }

        protected void AssertOutputContains(string outoutShouldContain)
        {
            Assert.True(Output.ToString().Contains(outoutShouldContain));
        }
    }


    public abstract class CmdLineErrorSpecificationBase : CmdLineSpecificationBase
    {
        protected abstract string Args { get; }
        protected abstract string ExpectErrorMessage { get; }

        protected override void Because()
        {
            parser = new SwitchParser(Output, packageTree);
            ParsedArgs = parser.Parse(new[] { Args });
            IsValid = parser.IsValid(ParsedArgs);
        }
    }
}