using System.Collections.Generic;

namespace Horn.Core.Spec.Unit.dsl
{
    public abstract class BaseDSLSpecification : Specification
    {
        protected const string DESCRIPTION = "This is a description of horn";

        protected const string SVN_URL = "https://svnserver/trunk";

        protected const string BUILD_FILE = "rakefile.rb";

        protected readonly List<string> TASKS = new List<string> {"build", "test", "deploy"};

    }
}