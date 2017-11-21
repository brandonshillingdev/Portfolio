using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public class UserFunctions : RepositoryClass<User>, iUser
    {
        /// <summary>
        /// User Functions
        /// </summary>
   
        public UserFunctions(DbContext dbcontext) : base(dbcontext)
        {
        }

        //create functions and add parameters       
        public bool CheckEmail(string Email)
        {
            //checks if email exists
            User user = GetALL().SingleOrDefault(e => e.Email.ToLower() == Email.ToLower());
            //returns if email exists
            return (user != null) ? true : false;
        }
        public bool CheckUsername(string Username)
        {
            //checks if username exists
            User user = GetALL().SingleOrDefault(e => e.Username.ToLower() == Username.ToLower());
            //returns if username exists
            return (user != null) ? true : false;
        }
        public List<UserShort> FillClientComboForAdmin(int adminId)
        {
            var clients = GetALL().Where(c => c.Type == "Client" && c.AdminId == adminId).ToList();
            //creates a list of usershort class
            List<UserShort> clientsh = new List<UserShort>();
            //fills list
            foreach (User u in clients)
            {
                clientsh.Add(new UserShort
                {
                    //sets information
                    FullName = u.FullName,
                    UserId = u.Id
                });
            }
            //returns list
            return clientsh;
        }
        public List<UserShort> FillPhotographerComboForAdmin(int adminId)
        {
            var photog = GetALL().Where(c => c.Type == "Photographer" && c.AdminId == adminId).ToList();
            //creates a list of usershort class
            List<UserShort> photogsh = new List<UserShort>();
            //fills list
            foreach (User u in photog)
            {
                photogsh.Add(new UserShort
                {
                    //sets user information
                    FullName = u.FullName,
                    UserId = u.Id
                });
            }
            //returns list
            return photogsh;
        }
        public List<UserShort> FillClientComboForPhotographer(int Id)
        {
            var photog = GetALL().Where(c => c.Type == "Client" && c.PhotographerID == Id).ToList();
            //creates a list of usershort class
            List<UserShort> photogsh = new List<UserShort>();
            //fills list
            foreach (User u in photog)
            {
                photogsh.Add(new UserShort
                {
                    //sets information
                    FullName = u.FullName,
                    UserId = u.Id
                });
            }
            //returns list
            return photogsh;
        }
        public List<User> GetClientsForPhotographer(int Id)
        {
            //retuns all clients from company
            return GetALL().Where(c => c.Type == "Client").Where(c => c.PhotographerID == Id).ToList();
        }
        public List<User> GetClientsForAdmin(int adminId)
        {
            //retuns all clients from company
            return GetALL().Where(c => c.Type == "Client").Where(c => c.AdminId == adminId).ToList();
        }
        public List<User> FillPhotographer(int adminId)
        {
            //returns all photographers from company
            return GetALL().Where(c => c.Type == "Photographer").Where(c => c.AdminId == adminId).ToList();
        }
        public User Login(string Username, string Password)
        {
            //checks login credentials
            User user = GetALL().SingleOrDefault(u => u.Username == Username && u.Password == Password);
            //checks if user exists
            if (user != null)
            {
                //checks if username matches exactly
                if (Username != user.Username)
                {
                    return null;
                }
                //checks if password matches exactly
                if (Password != user.Password)
                {
                    return null;
                }
            }
            //returns user
            return user;
        }
        public User GetUserByEmail(string email)
        {
            return GetALL().SingleOrDefault(u => u.Email == email);
        }
    }
}
