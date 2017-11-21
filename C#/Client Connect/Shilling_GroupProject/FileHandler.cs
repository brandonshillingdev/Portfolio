using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shilling_GroupProject
{
    public class FileHandler
    {
         public void createFolder(string filepath)
        {
            if (!Directory.Exists(filepath))
            {
                //creates folder if folder doesnt exist
                Directory.CreateDirectory(filepath);
            }
        }
    }
}
