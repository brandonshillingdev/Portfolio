using EntityCore;
using EntityCore.Interfaces;
using EntityCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCore.Functions
{
    public class UserFunctions :  iUser
    {
        /// <summary>
        /// User Functions
        /// </summary>
        DbModel db;

        public UserFunctions(DbModel db)
        {
            this.db = db;
        }

        //functions     
        public Users Login(string Username, string Password)
        {

            //checks login credentials
            Users user = db.Users.SingleOrDefault(u => u.Username == Username && u.Password == Password);
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
        public bool CheckEmail(string Email)
        {
            //checks if email exists
            Users user = db.Users.SingleOrDefault(e => e.Email.ToLower() == Email.ToLower());
            //returns if email exists
            return user != null;
        }     
        public bool CheckUsername(string Username)
        {
            //checks if username exists
            Users user = db.Users.SingleOrDefault(e => e.Username.ToLower() == Username.ToLower());
            //returns if username exists
            return user != null;
        }
        //gets a user by their email address
        public Users getUserByEmail(string email) => db.Users.SingleOrDefault(u => u.Email == email);
        public int GetUserIdByUsername(string username)
        {
            //gets the userid of a user by their username
            Users user = db.Users.SingleOrDefault(u => u.Username == username);
            return user.UserId;
        }


        public Users GetById(int id)
        {
            return db.Users.Find(id);
        }

    }
}
