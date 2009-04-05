using Rhino.DSL;

namespace Horn.Core.Dsl
{
    using Boo.Lang.Compiler.Steps;

    public class ConfigReaderEngine : DslEngine
    {

        protected override void CustomizeCompiler(Boo.Lang.Compiler.BooCompiler compiler, Boo.Lang.Compiler.CompilerPipeline pipeline, string[] urls)
        {
            pipeline.Insert(1, new ImplicitBaseClassCompilerStep(typeof(BooConfigReader), "Prepare", "Horn.Core.Dsl"));
            pipeline.InsertBefore(typeof(ProcessMethodBodiesWithDuckTyping), new RightShiftToMethodCompilerStep());
            pipeline.Insert(2, new UnderscorNamingConventionsToPascalCaseCompilerStep());
            pipeline.Insert(3, new UseSymbolsStep());
            
        }



    }
}