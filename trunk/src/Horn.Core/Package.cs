using Horn.Core.dsl;

namespace Horn.Core
{
    public class Package
    {
        public string Name { get; private set; }

        public IBuildMetaData BuildMetaData{ get; private set;}

        public Package(string name, IBuildMetaData buildMetaData)
        {
            BuildMetaData = buildMetaData;

            Name = name;
        }
    }
}