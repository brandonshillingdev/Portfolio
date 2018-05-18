using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    //vars
    public enum Privacy { Public, Friends };
    public enum Status { Active, Deleted };

    public class PostInfo
    {
        public PostInfo()
        {
            Pictures = new List<Image>();
        }

        public Privacy privacy { get; set; }
        public Status status { get; set; }
        public Byte[] Program { get; set; }
        public Byte[] SourceCode { get; set; }
        public List<Image> Pictures { get; set; }
        public int ThisPostId { get; set; }

    }
}
