using System.Collections.Generic;
using System.IO;
using Horn.Core.Utils.CmdLine;
using Horn.Domain.PackageStructure;
using Horn.Domain.Spec.helpers;
using Xunit;

namespace Horn.Domain.Spec.Unit.CmdLine
{
    public abstract class CmdLineSpecificationBase : Specification
    {
        private TextWriter textWriter;
        protected SwitchParser parser;
        protected IPackageTree packageTree;

        protected IDictionary<string, IList<string>> ParsedArgs { get; set; }

        protected TextWriter Output { get { return textWriter; } }

        protected bool IsValid { get; set; }

        protected override void Before_each_spec()
        {
            base.Before_each_spec();

            textWriter = new StringWriter();

            packageTree = TreeHelper.GetTempPackageTree();
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