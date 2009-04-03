using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using ddd.belfast.ioc;

namespace ddd.belfast
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