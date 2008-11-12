using Horn.Core.Utils.CmdLine;
using Xunit;

namespace Horn.Core.Spec.Unit.CmdLine
{
    public class When_Horn_Recevies_An_Install_Switch_From_The_Command_Line : CmdLineSpecificationBase
    {
        private const string arg = "-install:horn";
        private const string installName = "horn";

        protected override void Because()
        {
            parser = new SwitchParser(Output);

            ParsedArgs = parser.Parse(new[]{arg});
            IsValid = parser.IsValid(ParsedArgs);
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Valid()
        {
            Assert.True(IsValid);
        }

        [Fact]
        public void Then_Parsed_Argumens_Contain_The_Install_Name()
        {
            Assert.Equal(installName, ParsedArgs["install"][0]);
        }
    }


    public class When_Horn_Receives_No_Command_Line_Arguments : CmdLineErrorSpecificationBase
    {
        protected override string Args
        {
            get { return ""; }
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
            Assert.True(Output.ToString().Contains(ExpectErrorMessage));
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
            Assert.True(Output.ToString().Contains(ExpectErrorMessage));
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
            get { return "Argument install has already been given the value: ."; }
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Not_Valid()
        {
            Assert.False(IsValid);
        }

        [Fact]
        public void Then_Should_Output_Argument_Has_Already_Been_Given_The_Value_Error_Message()
        {
            Assert.True(Output.ToString().Contains(ExpectErrorMessage));
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
            get { return "Argument install has already been given the value: unknown."; }
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Not_Valid()
        {
            Assert.False(IsValid);
        }

        [Fact]
        public void Then_Should_Output_Argument_Has_Already_Been_Given_The_Value_Error_Message()
        {
            Assert.True(Output.ToString().Contains(ExpectErrorMessage));
        }
    }

    public class When_Horn_Recevies_Two_Install_Arguments : CmdLineErrorSpecificationBase
    {
        protected override string Args
        {
            get { return "-install:horn -install:horn"; }
        }

        protected override string ExpectErrorMessage
        {
            get { return "Argument install has already been given the value: horn -install:horn."; }
        }

        [Fact]
        public void Then_Parsed_Arguments_Are_Not_Valid()
        {
            Assert.False(IsValid);
        }

        [Fact]
        public void Then_Should_Output_Argument_Has_Already_Been_Given_The_Value_Error_Message()
        {
            Assert.True(Output.ToString().Contains(ExpectErrorMessage));
        }
    }
}