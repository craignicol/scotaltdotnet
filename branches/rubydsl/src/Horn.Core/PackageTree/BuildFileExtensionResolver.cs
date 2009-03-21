using System.IO;

namespace Horn.Core.PackageStructure
{
    public class BuildFileExtensionResolver : IBuildFileExtensionResolver
    {

        private string filePattern = "build.*";


        public string Resolve(DirectoryInfo buildFolder)
        {
            var files = Directory.GetFiles(buildFolder.FullName, filePattern);

            if (files.Length == 0)
                throw new MissingBuildFileException(string.Format("No build.boo file component {0} at path {1}.", buildFolder.Name, buildFolder.FullName));

            if (files.Length > 1)
                throw new InvalidDataException(string.Format("More than one build file exists for component {0} at path {1}.", buildFolder.Name, buildFolder.FullName));

            return Path.GetExtension(files[0]).Substring(1);
        }

        public void SetFilePattern(string mask)
        {
            filePattern = mask;
        }



    }
}