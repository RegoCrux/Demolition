using System.IO;
using System.Web;

namespace Demolition.Test
{
    public class FakeFileWrapper : HttpPostedFileBase
    {
        string fullPath;

        public FakeFileWrapper(string fullpath)
        {
            this.fullPath = fullpath;
        }

        public override string FileName
        {
            get
            {
                return Path.GetFileName(fullPath);
            }
        }

        public override void SaveAs(string filename)
        {
            File.Copy(this.fullPath, filename);
        }

        public override Stream InputStream
        {
            get
            {
                return new StreamReader(this.fullPath).BaseStream;
            }
        }
    }
}
