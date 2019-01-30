using System;
using EntityCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityCore.Model;

namespace EntityCore.Functions
{
    public class FriendshipFunctions :  iFriendship
    {
        DbModel db;

        public FriendshipFunctions(DbModel db)
        {
            this.db = db;
        }

        public Friendship Add(Friendship friendship)
        {
            return db.Friendship.Add(friendship).Entity;
        }

        public Friendship GetById(int id)
        {
            return db.Friendship.Find(id);
        }

        public void Update(Friendship friendship)
        {
            db.Friendship.Update(friendship);
        }

        public List<Friendship> GetFriends(int CurrentUserId)
        {
            //gets all friends for the current user
            return db.Friendship.Where(f => (f.UserId1 == CurrentUserId || f.UserId2 == CurrentUserId) && f.Status == 1).ToList();
        }

        public Friendship GetFriendship(int userid1, int userid2)
        {
            return db.Friendship.SingleOrDefault(f=> f.UserId1 == userid1 && f.UserId2 == userid2);
        }

        public List<Friendship> GetPendingFriendRequests(int CurrentUserId)
        {
            //gets all friend requests for the current user
            return db.Friendship
                .Where(f => (
                (f.UserId1 == CurrentUserId || 
                f.UserId2 == CurrentUserId)
                && f.Status == 0 
                && f.ActionUserId != CurrentUserId
                )).ToList();
        }

        public List<Friendship> GetPendingFriendRequestsOut(int CurrentUserId)
        {
            //gets all friend requests for the current user
            return db.Friendship
                .Where(f => (
                (f.UserId1 == CurrentUserId ||
                f.UserId2 == CurrentUserId)
                && f.Status == 0
                && f.ActionUserId == CurrentUserId
                )).ToList();
        }
    }
}
