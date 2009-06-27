using System;
using System.Collections.Generic;

namespace core.domain
{
    public class User
    {
        public User()
        {
            Permissions = new Dictionary<string, Permission>();
        }

        public virtual Guid Uid { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string Surname { get; set; }

        public virtual string Email { get; set; }

        public Role Role { get; set; }

        public IDictionary<string, Permission> Permissions { get; set; }
    }
}