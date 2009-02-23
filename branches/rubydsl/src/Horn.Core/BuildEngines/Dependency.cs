namespace Horn.Core.BuildEngines
{
    public class Dependency
    {
        public string Name { get; private set; }
        public string Location { get; private set; }

        public Dependency(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }
}