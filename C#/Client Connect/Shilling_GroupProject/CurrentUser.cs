using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOps2;

namespace Shilling_GroupProject
{
    public class CurrentUser
    {
        public static User user;

        public static void logout()
        {
            //logs out user
            user = null;
        }
    }
}
