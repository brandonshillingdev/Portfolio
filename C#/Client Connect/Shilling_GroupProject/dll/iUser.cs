using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public interface iUser : iRepository<User>
    {
        //create functions and add parameters
        //for users
        User Login(string Username, string Password);
        User GetUserByEmail(string email);
        bool CheckEmail(string email);
        bool CheckUsername(string username);
        List<User> FillPhotographer(int adminId);
        List<User> GetClientsForAdmin(int adminId);
        List<User> GetClientsForPhotographer(int Id);
        List<UserShort> FillClientComboForAdmin(int adminId);
        List<UserShort> FillPhotographerComboForAdmin(int adminId);
        List<UserShort> FillClientComboForPhotographer(int Id);
    }
}
