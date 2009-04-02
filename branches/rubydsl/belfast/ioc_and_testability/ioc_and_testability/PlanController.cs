using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using ddd.belfast.ioc;

namespace ddd.belfast
{
    public class PlanController : IController 
    {

        private readonly IPlanDao _planDao;
        private readonly IDocumentService _documentService;
        private readonly IEmailSender _sender;


        public void Execute(RequestContext requestContext)
        {
        }



        public PlanController(IPlanDao planDao, IDocumentService distributionService, IEmailSender sender)
        {
            _planDao = planDao;
            _documentService = distributionService;
            _sender = sender;
        }

        public void CheckIn(Guid planUid)
        {
            var plan = _planDao.GetPlan(planUid);

            var planInstancePath = _documentService.GetDocumentInstance(plan);

            var list = string.Join(";", plan.DistributionList.Select(x => x.Email).ToArray()).Trim(';');

            _sender.Send("admin@here.com", list, "Plan has been checked in", "The plan was checked in", new[]{planInstancePath});
        }
    }

    public interface IDistributionService
    {
    }

    public  interface IDocumentService
    {
        string GetDocumentInstance(Plan plan);
    }

    public interface IPlanDao
    {
        Plan GetPlan(Guid planUid);
    }

    public class DistributionService
    {

        public int GetEmailList()
        {
            throw new NotImplementedException();
        }



    }

    public class DocumentService
    {

        public int GetPlanDocumentInstance()
        {
            throw new NotImplementedException();
        }



    }

    public class PlanDao : IPlanDao
    {
        public Plan GetPlan(Guid planUid)
        {
            throw new NotImplementedException();
        }
    }
}