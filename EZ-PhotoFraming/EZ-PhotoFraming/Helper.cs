using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PhotoFraming
{
    class Helper
    {
        static public Boolean ParsePath(String FullPath, out String Directory, out String FileName, out String Extension)
        {
            String dir = Path.GetDirectoryName(FullPath);
            if (dir == String.Empty)
            {
                Directory = String.Empty;
                FileName = String.Empty;
                Extension = String.Empty;
                return false;
            }

            String ext = Path.GetExtension(FullPath);
            if (ext == String.Empty)
            {
                Directory = FullPath;
                FileName = String.Empty;
                Extension = String.Empty;
                return true;
            }
            else
            {
                Directory = dir;
                FileName = Path.GetFileName(FullPath);
                Extension = ext;
                return true;
            }
        }
    }
}
