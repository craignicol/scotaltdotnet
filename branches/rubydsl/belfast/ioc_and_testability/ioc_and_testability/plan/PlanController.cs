using System.Web.Mvc;
using ddd.belfast.ioc;

namespace ddd.belfast.plan
{
    public class PlanController : Controller 
    {
        private readonly IPlanDao _planDao;


        public PlanController(IPlanDao planDao)
        {
            _planDao = planDao;
        }
    }
}