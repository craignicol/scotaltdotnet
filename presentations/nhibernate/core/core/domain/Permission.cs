namespace core.domain
{
    public class Permission
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public Permission()
        {
            Id = -1;
        }        
    }
}