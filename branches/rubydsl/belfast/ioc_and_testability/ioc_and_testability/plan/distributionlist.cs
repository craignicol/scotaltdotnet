using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ddd.belfast.ioc
{
    [TestFixture]
    public class When_validating_a_plan : ContextSpecification
    {
        private Plan _plan;
        private Contact _contact;

        protected override void establish_context()
        {
            _plan = new Plan("Pandemic Bird Flu");

            _contact = new Contact("dagda1@scotalt.net");
        }

        protected override void because()
        {
            _plan.DistributionList.Add(_contact);
        }

        [Test]
        public void Then_the_distribution_list_should_have_contacts()
        {
            Assert.That(_plan.IsValid, Is.True);
        }
    }

    public class Contact
    {
        private readonly string _email;

        public Contact(string email)
        {
            _email = email;
        }

        public string Email
        {
            get { return _email; }
        }
    }

    public class Plan
    {
        private readonly Guid _uid;
        private readonly string _title;
        private bool _isValid;

        public Plan(string title)
        {
            _title = title;

            DistributionList = new List<Contact>();
        }

        public Plan(Guid uid, string title)
        {
            _uid = uid;
            _title = title;
        }

        public IList<Contact> DistributionList
        {
            get; set;
        }

        public bool IsValid
        {
            get
            {
                return (DistributionList.Count > 0);
            }
        }

        public Guid Uid
        {
            get { return _uid; }
        }

        public string Title
        {
            get { return _title; }
        }
    }
}