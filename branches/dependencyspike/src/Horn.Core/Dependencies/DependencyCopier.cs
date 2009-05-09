namespace Horn.Core.Dependencies
{
    using System.Collections.Generic;
    using System.IO;
    using extensions;
    using Utils;

    public class DependencyCopier : WithLogging
    {
        public IEnumerable<string> CopyDependency(FileInfo file, DirectoryInfo destination)
        {
            IEnumerable<string> locationsToCopyTo = destination.Search(file.Name);

            foreach (var location in locationsToCopyTo)
            {
                CopyFile(file, location);
            }

            return locationsToCopyTo;
        }

        private void CopyFile(FileInfo nextFile, string location)
        {
            InfoFormat("Dependency: Copying {0} to {1} ...", nextFile.FullName, location);
            nextFile.CopyTo(location, true);
        }
    }
}