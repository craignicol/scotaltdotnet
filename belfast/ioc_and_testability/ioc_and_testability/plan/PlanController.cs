using System.Web.Mvc;
using ddd.belfast.ioc;

namespace ddd.belfast.plan
{
    public class PlanController : Controller 
    {
        private readonly IPlanDao _planDao;
        private readonly IEmailSender _sender;



        public PlanController(IPlanDao planDao, IEmailSender sender)
        {
            _planDao = planDao;
            _sender = sender;
        }
    }
}