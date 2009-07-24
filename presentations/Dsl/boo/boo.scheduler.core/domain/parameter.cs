namespace boo.scheduler.core.domain
{
    public class Parameter
    {
        public string Name { get; private set; }

        public Parameter(string name)
        {
            Name = name;
        }
    }
}