using System.Collections.Generic;
using Horn.Core.Utils.CmdLine;
using Xunit;

namespace Horn.Core.Spec.Unit.CmdLine
{
    public class When_Horn_Recevies_An_Install_Switch_From_The_Command_Line : Specification
    {
        private string arg;

        protected override void Because()
        {
            arg = "-install:horn";
        }

        [Fact]
        public void Then_Horn_Parses_The_Name_Of_The_Component_To_Install()
        {
            IDictionary<string, IList<string>> parsedArgs = SwitchParser.Parse(new[]{arg});

            Assert.True(SwitchParser.IsValid(parsedArgs));
        }
    }

    public class When_Horn_Receives_No_Command_Line_Arguments : Specification
    {
        protected override void Because()
        {
        }

        [Fact]
        public void Then_Horn_Stops_Execution_And_Outputs_To_The_Console()
        {
            IDictionary<string, IList<string>> parsedArgs = SwitchParser.Parse(new string[] { });

            Assert.False(SwitchParser.IsValid(parsedArgs));
        }
    }

    public class When_Horn_Receives_No_Install_Argument : Specification
    {
        private string invalidArgs;

        protected override void Because()
        {
            invalidArgs = "-somearg:something";
        }

        [Fact]
        public void Then_Horn_Stops_Execution_And_Outputs_To_The_Console()
        {
            IDictionary<string, IList<string>> parsedArgs = SwitchParser.Parse(new[] { invalidArgs });

            Assert.False(SwitchParser.IsValid(parsedArgs));            
        }
    }

    public class When_Horn_Recevies_Install_Argument_With_No_Value : Specification
    {
        private string invalidArgs;

        protected override void Because()
        {
            invalidArgs = "-install:";
        }

        [Fact]
        public void Then_Horn_Stops_Execution_And_Outputs_To_The_Console()
        {
            IDictionary<string, IList<string>> parsedArgs = SwitchParser.Parse(new[] { invalidArgs });

            Assert.False(SwitchParser.IsValid(parsedArgs));
        }        
    }

    public class When_Horn_Recevies_Install_Argument_With_An_Unrecognised_Component : Specification
    {
        private string invalidArgs;

        protected override void Because()
        {
            invalidArgs = "-install:unknown";
        }

        [Fact]
        public void Then_Horn_Stops_Execution_And_Outputs_To_The_Console()
        {
            IDictionary<string, IList<string>> parsedArgs = SwitchParser.Parse(new[] { invalidArgs });

            Assert.False(SwitchParser.IsValid(parsedArgs));
        }
    }

    public class When_Horn_Recevies_Two_Install_Arguments: Specification
    {
        private string invalidArgs;

        protected override void Because()
        {
            invalidArgs = "-install:horn -install:horn";
        }

        [Fact]
        public void Then_Horn_Stops_Execution_And_Outputs_To_The_Console()
        {
            IDictionary<string, IList<string>> parsedArgs = SwitchParser.Parse(new[] { invalidArgs });

            Assert.False(SwitchParser.IsValid(parsedArgs));
        }
    }
}