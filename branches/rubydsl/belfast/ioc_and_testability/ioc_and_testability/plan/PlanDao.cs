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
            return new Plan(Guid.NewGuid(), "Test Plan");
        }
    }
}