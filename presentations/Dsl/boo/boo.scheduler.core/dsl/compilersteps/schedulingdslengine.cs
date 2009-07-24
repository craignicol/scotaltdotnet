using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Steps;
using Rhino.DSL;

namespace boo.scheduler.core.dsl.compilersteps
{
    public class SchedulerDslEngine : DslEngine
    {
        protected override void CustomizeCompiler(BooCompiler compiler, CompilerPipeline pipeline, string[] urls)
        {
            pipeline.Insert(1,
                            new ImplicitBaseClassCompilerStep(typeof(BaseScheduler), "Prepare",
                                                              "boo.scheduler.core.domain"));

            pipeline.InsertBefore(typeof(ProcessMethodBodiesWithDuckTyping), new UnderscoreNamingConventionsToPascalCaseCompilerStep());
            pipeline.Insert(3, new UseSymbolsStep());
        }
    }
}