using System;
using System.Collections.Generic;
using System.IO;
using Horn.Domain.Exceptions;
using Horn.Domain.Utils;
namespace Horn.Domain.PackageStructure
{
    public class BuildFileResolver : IBuildFileResolver
    {

        private readonly string[] allowedFileExtensions = new[]{"boo", "rb"};//JFHCI

        private string buildFile;


        public string BuildFile
        {
            get { return buildFile; }
        }

        public string Extension
        {
            get
            {
                if (string.IsNullOrEmpty(buildFile))
                    throw new Exception("The file path has not been set for the BuildFileResolver");

                return Path.GetExtension(buildFile).Substring(1);
            }
        }



        public BuildFileResolver Resolve(DirectoryInfo buildFolder, string fileName)
        {
            var buildFiles = new List<string>();

            allowedFileExtensions.ForEach(extension =>
                                              {
                                                  var file = Path.Combine(buildFolder.FullName,
                                                                          string.Format("{0}.{1}", fileName, extension));

                                                  if(File.Exists(file))
                                                      buildFiles.Add(file);
                                              });
            if(buildFiles.Count == 0)
                throw new MissingBuildFileException(buildFolder);

            if(buildFiles.Count != 1)
                throw new InvalidDataException(string.Format("More than one build file exists for {0} in folder {1}", buildFolder.Name, buildFolder.FullName));

            buildFile = buildFiles[0];

            return this;
        }



    }
}