using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Steps;
using Rhino.DSL;

namespace boo.scheduler.core.dsl.compilersteps
{
    /// <summary>
    /// The rhino DslEngine takes care of a lot of boilerplate code and some extension points.
    /// </summary>
    public class SchedulerDslEngine : DslEngine
    {
        protected override void CustomizeCompiler(BooCompiler compiler, CompilerPipeline pipeline, string[] urls)
        {
            //boo compiler trickery via the annonymous base class pattern.
            pipeline.Insert(1,
                            new ImplicitBaseClassCompilerStep(typeof(BaseScheduler), "Prepare",
                                                              "boo.scheduler.core.domain"));

            pipeline.InsertBefore(typeof(ProcessMethodBodiesWithDuckTyping), new UnderscoreNamingConventionsToPascalCaseCompilerStep());
            pipeline.Insert(3, new UseSymbolsStep());   //allows us to create a symbol like syntax using the @ character
        }
    }
}