using EntityCore;
using EntityCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCore.Interfaces
{
    public interface iUser
    {
        //create functions
        Users Login(string Username, string Password);
        bool CheckEmail(string email);
        bool CheckUsername(string username);
        Users getUserByEmail(string email);
        int GetUserIdByUsername(string username);
    }
}
