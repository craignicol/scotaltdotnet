using System;
using System.IO;
using System.Text;
using log4net;

namespace Horn.Domain.PackageStructure
{
    public class RevisionData : IRevisionData
    {

        private readonly FileInfo revisionFileInfo;
        private string revision;
        private static readonly ILog log = LogManager.GetLogger(typeof (RevisionData));
        public const string FILE_NAME = "revision.horn";


        public bool Exists
        {
            get { return revisionFileInfo.Exists; }
        }

        public string Revision
        {
            get
            {
                if (!string.IsNullOrEmpty(revision))
                    return revision;

                try
                {
                    using(var stream =  revisionFileInfo.OpenRead())
                    {
                        byte[] b = new byte[1024];
                        UTF8Encoding temp = new UTF8Encoding(true);

                        while (stream.Read(b, 0, b.Length) > 0)
                        {
                            revision = temp.GetString(b).Trim(new[]{'\r', '\n', '\0'}).Split('=')[1];
                        }

                    }
                }
                catch(IOException ioe)
                {
                    log.Error(ioe);

                    return "0";
                }

                return revision;
            }
        }



        public virtual void RecordRevision(IPackageTree packageTree, string revisionVlaue)
        {
            var fileInfo = GetRevisionFile(packageTree);

            RecordRevision(fileInfo, revisionVlaue);
        }

        public bool ShouldUpdate(IRevisionData other)
        {
            return (long.Parse(other.Revision) > long.Parse(Revision));
        }



        private void RecordRevision(FileInfo fileInfo, string revisionValue)
        {
            using (var stream = fileInfo.OpenWrite())
            {
                Byte[] info =
                    new UTF8Encoding(true).GetBytes(string.Format("revision={0}", revisionValue));

                stream.Write(info, 0, info.Length);
            }
        }

        private FileInfo GetRevisionFile(IPackageTree packageTree)
        {
            var file = Path.Combine(packageTree.CurrentDirectory.FullName, FILE_NAME);

            return new FileInfo(file);
        }



        public RevisionData(string revision)
        {
            this.revision = revision;
        }

        public RevisionData(IPackageTree packageTree)
        {
            revisionFileInfo = GetRevisionFile(packageTree);

            if (revisionFileInfo.Exists) 
                return;

            RecordRevision(revisionFileInfo, "0");
        }



    }
}