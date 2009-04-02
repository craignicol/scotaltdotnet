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
        }



        public PlanController(IPlanDao planDao, 
                                IDocumentService distributionService )
        {
            _planDao = planDao;
            _documentService = distributionService;
        }
    }

    public interface IDistributionService
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