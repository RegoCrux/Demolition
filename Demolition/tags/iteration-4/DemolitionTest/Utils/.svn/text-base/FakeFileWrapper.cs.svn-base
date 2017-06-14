using System.IO;
using System.Web;

namespace Demolition.Test
{
    class FakeFileWrapper : HttpPostedFileBase
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
    }
}
