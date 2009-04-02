using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;

namespace ddd.belfast.ioc
{
    [TestFixture]
    public class When_a_plan_is_checked_in : ContextSpecification
    {
        private Plan _plan;
        private PlanController _planController;
        private IPlanDao _planDao;
        private IDocumentService _documentService;
        private IEmailSender _emailSender;
        private bool _result;

        private MockRepository _mockRepository;

        protected override void establish_context()
        {
            _plan = new Plan(Guid.NewGuid(), "Pandemic Bird Flu");

            _plan.DistributionList = new List<Contact>{new Contact("dagda1@scotalt.net"), new Contact("paul.cowan@continuity2.com")};

            _mockRepository = new MockRepository();

            _planDao = MockRepository.GenerateStub<IPlanDao>();
            _documentService = MockRepository.GenerateStub<IDocumentService>();
            _emailSender = MockRepository.GenerateStub<IEmailSender>();
            _planController = new PlanController(_planDao, _documentService, _emailSender);

            _planDao.Stub(x => x.GetPlan(_plan.Uid)).Return(_plan);
            _documentService.Stub(x => x.GetDocumentInstance(_plan)).Return(@"C:\someplan.doc");
        }

        protected override void because()
        {
            _mockRepository.Playback();

            _result = _planController.CheckIn(_plan.Uid);
        }

        [Test]
        public void Then_the_distribution_list_are_emailed()
        {
            _emailSender.AssertWasCalled(
                x =>
                x.Send(Arg<string>.Is.TypeOf, Arg<string>.Is.TypeOf, Arg<string>.Is.TypeOf, Arg<string>.Is.TypeOf,
                       Arg<string[]>.Is.TypeOf));
        }
    }
}