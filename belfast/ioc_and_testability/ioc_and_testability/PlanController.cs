using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ddd.belfast
{
    public class PlanController : IController 
    {

        private readonly IPlanDao _planDao;
        private readonly IDocumentService _documentService;


        public void Execute(RequestContext requestContext)
        {
            var i = _planDao.GetPlan();
            var j = _documentService.GetEmailList();
            var k = _documentService.GetPlanDocumentInstance();
        }



        public PlanController(IPlanDao planDao, IDocumentService distributionService )
        {
            _planDao = planDao;
            _documentService = distributionService;
        }
    }

    public internal interface IDistributionService
    {
    }

    public  interface IDocumentService
    {
    }

    public interface IPlanDao
    {
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

        public int GetPlan()
        {
            throw new NotImplementedException();
        }



    }
}