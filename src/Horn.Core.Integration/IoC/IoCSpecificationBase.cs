using Horn.Core.Utils.IoC;

namespace Horn.Core.Spec.Integration
{
    public class IoCSpecificationBase : Specification
    {

        protected override void Before_each_spec()
        {
            var resolver = new WindsorDependencyResolver();

            IoC.InitializeWith(resolver);            
        }

        protected override void Because()
        {
        }


        
    }
}