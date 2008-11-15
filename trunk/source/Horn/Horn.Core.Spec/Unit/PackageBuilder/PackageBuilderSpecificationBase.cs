using System.Collections.Generic;

namespace Horn.Core.Spec.Unit.PackageBuilderSpec
{
    public abstract class PackageBuilderSpecificationBase : Specification
    {
        protected IDictionary<string, IList<string>> switches = new Dictionary<string, IList<string>>();

        protected override void Before_each_spec()
        {
            
        }
    }
}