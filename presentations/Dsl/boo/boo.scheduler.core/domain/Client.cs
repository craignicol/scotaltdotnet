namespace boo.scheduler.core.domain
{
    public class Client
    {
        public string Name { get; private set; }

        public Client(string name)
        {
            Name = name;
        }
    }
}