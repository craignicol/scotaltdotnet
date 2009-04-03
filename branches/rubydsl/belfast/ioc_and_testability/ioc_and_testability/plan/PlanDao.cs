using System;

namespace ddd.belfast.plan
{
    public interface IPlanDao
    {
    }

    public class PlanDao : IPlanDao
    {
        public Plan GetPlan(Guid planUid)
        {
            var plan =  new Plan(Guid.NewGuid(), "Test Plan");

            plan.DistributionList.Add(new Contact("dagda1@scotalt.net"));
            plan.DistributionList.Add(new Contact("paul.cowan@continuity2.com"));

            return plan;
        }
    }
}