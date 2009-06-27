namespace core.domain
{
    public class Role
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public Role()
        {
            Id = -1;
        }
    }
}