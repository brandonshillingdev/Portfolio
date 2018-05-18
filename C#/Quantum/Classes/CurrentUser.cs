using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCore.Model;
namespace Quantum
{
    public class CurrentUser
    {
        //vars
        private static DbUnit unit = new DbUnit();
        public static Users user;

        public static void logout()
        {
            //logs out user
            user = null;
        }
        public static void update()
        {
            //updates the current user
            user = unit.Users.GetById(user.UserId);
        }
    }
}
