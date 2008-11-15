using System.Collections.Generic;

namespace Horn.Core.Spec.Unit.PackageCommands
{
    public abstract class PackageCommandSpecificationBase : Specification
    {
        protected IDictionary<string, IList<string>> switches = new Dictionary<string, IList<string>>();

        protected override void Before_each_spec()
        {
            
        }
    }
}