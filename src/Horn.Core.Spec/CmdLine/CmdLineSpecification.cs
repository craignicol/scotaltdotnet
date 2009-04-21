using Horn.Core.Utils.CmdLine;
using Xunit;

namespace Horn.Core.Spec.Unit.CmdLine
{
    public class When_horn_recevies_an_Install_Switch_From_The_Command_Line : CmdLineSpecificationBase
    {
        private const string arg = "-install:horn";
        private const string installName = "horn";

        protected override void Because()
        {
            parser = new SwitchParser(Output, packageTree);

            ParsedArgs = parser.Parse(new[]{arg});
            IsValid = parser.IsValid(ParsedArgs);
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Valid()
        {
            Assert.True(IsValid);
        }

        [Fact]
        public void Then_Parsed_Arguments_Contain_The_Install_Name()
        {
            Assert.Equal(installName, ParsedArgs["install"][0]);
        }
    }

    public class When_Horn_Receives_The_Help_Switch : CmdLineSpecificationBase
    {
        private const string arg = "-help";

        protected override void Because()
        {
            parser = new SwitchParser(Output, packageTree);

            ParsedArgs = parser.Parse(new[] { arg });
        }

        [Fact]
        public void Then_Console_Should_Output_Help_Text()
        {
            AssertOutputContains(SwitchParser.HELP_TEXT);
        }

        [Fact]
        public void Then_A_Help_Return_Value_Is_returned()
        {
            Assert.IsAssignableFrom<HelpReturnValue>(ParsedArgs);
        }
    }


    public class When_Horn_Receives_No_Command_Line_Arguments : CmdLineErrorSpecificationBase
    {
        protected override string Args
        {
            get { return string.Empty; }
        }

        protected override string ExpectErrorMessage
        {
            get { return "Missing required argument key"; }
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Not_Valid()
        {
            Assert.False(IsValid);
        }

        [Fact]
        public void Then_Horn_Outputs_A_Missing_argument_Error_Message()
        {
            AssertOutputContains(ExpectErrorMessage);
        }
    }

    public class When_Horn_Receives_No_Install_Argument : CmdLineErrorSpecificationBase
    {
        protected override string Args
        {
            get { return "-somearg:something"; }
        }

        protected override string ExpectErrorMessage
        {
            get { return "Missing required argument key"; }
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Not_Valid()
        {
            Assert.False(IsValid);
        }

        [Fact]
        public void Then_Should_Output_Missing_argument_Error_Message()
        {
            AssertOutputContains(ExpectErrorMessage);
        }
    }


    public class When_Horn_Recevies_Install_Argument_With_No_Value : CmdLineErrorSpecificationBase
    {
        protected override string Args
        {
            get { return "-install:"; }
        }

        protected override string ExpectErrorMessage
        {
            get { return "Missing argument value for key: install."; }
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Not_Valid()
        {
            Assert.False(IsValid);
        }

        [Fact]
        public void Then_Should_Output_Argument_Has_Already_Been_Given_The_Value_Error_Message()
        {
            AssertOutputContains(ExpectErrorMessage);
        }
    }

    public class When_Horn_Recevies_Install_Argument_With_An_Unrecognised_Component : CmdLineErrorSpecificationBase
    {
        protected override string Args
        {
            get { return "-install:unknown"; }
        }

        protected override string ExpectErrorMessage
        {
            get { return "Argument value for key install is invalid: unknown."; }
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Not_Valid()
        {
            Assert.False(IsValid);
        }

        [Fact]
        public void Then_Should_Output_Argument_Has_Already_Been_Given_The_Value_Error_Message()
        {
            AssertOutputContains(ExpectErrorMessage);
        }
    }
}