using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public interface iUnit
    {
        void Commit();
        //gets function classes
        UserFunctions userfunction { get; }
        PhotoFunctions photofunction { get; }
    }
}
