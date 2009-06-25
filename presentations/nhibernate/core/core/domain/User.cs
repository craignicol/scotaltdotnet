using System;

namespace core.domain
{
    public class User
    {
        public virtual Guid Uid { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string Surname { get; set; }

        public virtual string Email { get; set; }
    }
}