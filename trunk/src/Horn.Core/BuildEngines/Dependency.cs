namespace Horn.Core.BuildEngines
{
    public class Dependency
    {

        public string Library { get; private set; }

        public string PackageName { get; private set; }



        public Dependency(string package, string library)
        {
            PackageName = package;
            Library = library;
        }



    }
}