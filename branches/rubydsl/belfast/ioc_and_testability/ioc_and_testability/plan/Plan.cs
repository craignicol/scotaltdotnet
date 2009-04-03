using System;

namespace ddd.belfast
{
    public class Plan
    {

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
        }



    }
}