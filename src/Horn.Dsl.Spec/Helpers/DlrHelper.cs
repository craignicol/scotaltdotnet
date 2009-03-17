using Horn.Core.Dsl;
using IronRuby;

namespace Horn.Dsl.Spec.Helpers
{
    public static class DlrHelper
    {
        public static BuildMetaData RetrieveBuildMetaDataFromTheDlr(string buildFilePath, string variableName, string methodName)
        {
            var engine = Ruby.CreateEngine();

            engine.Runtime.LoadAssembly(typeof(BuildMetaData).Assembly);

            engine.ExecuteFile(buildFilePath);

            var klass = engine.Runtime.Globals.GetVariable(variableName);

            var instance = engine.Operations.CreateInstance(klass);

            return  (BuildMetaData)engine.Operations.InvokeMember(instance, methodName);
        }        
    }
}