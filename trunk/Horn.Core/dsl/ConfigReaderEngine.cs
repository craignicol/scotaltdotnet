using Rhino.DSL;

namespace Horn.Core.dsl
{
    using Boo.Lang.Compiler.Steps;

    public class ConfigReaderEngine : DslEngine
    {
        protected override void CustomizeCompiler(Boo.Lang.Compiler.BooCompiler compiler, Boo.Lang.Compiler.CompilerPipeline pipeline, string[] urls)
        {
            pipeline.Insert(1, new ImplicitBaseClassCompilerStep(typeof(BaseConfigReader), "Prepare", "Horn.Core.dsl"));
            pipeline.InsertBefore(typeof(ProcessMethodBodiesWithDuckTyping), new UnderscorNamingConventionsToPascalCaseCompilerStep());
        }
    }
}