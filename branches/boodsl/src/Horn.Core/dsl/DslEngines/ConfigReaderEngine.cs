using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Steps;
using Rhino.DSL;
namespace Horn.Core.Dsl
{    
    public class ConfigReaderEngine : DslEngine
    {

        protected override void CustomizeCompiler(BooCompiler compiler, CompilerPipeline pipeline, string[] urls)
        {
            pipeline.Insert(1, new ImplicitBaseClassCompilerStep(typeof(Horn.Dsl.BooConfigReader), "Prepare", "Horn.Dsl"));
            pipeline.InsertBefore(typeof(ProcessMethodBodiesWithDuckTyping), new RightShiftToMethodCompilerStep());
            pipeline.Insert(2, new UseSymbolsStep());
            
        }



    }
}