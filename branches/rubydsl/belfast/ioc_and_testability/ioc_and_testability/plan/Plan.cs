using System;
using System.Collections.Generic;

namespace ddd.belfast.plan
{
    public class Plan
    {
        public IList<Contact> DistributionList { get; private set; }

        public string Title{ get; private set; }

        public Guid Uid { get; private set; }

        public Plan(string title)
        {
            Title = title;
        }

        public Plan(Guid uid)
        {
            Uid = uid;
        }

        public Plan(Guid uid, string title)
        {
            Uid = uid;
            Title = title;
            DistributionList = new List<Contact>();
        }
    }
}