using EntityCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCore.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityCore.Functions
{
    public class PostFunctions : iPost
    {
        DbModel db;

        public PostFunctions(DbModel db)
        {
            this.db = db;
        }

        public List<Posts> GetCommunityPosts(int skip, int take)
        {
            //gets all public posts and pages database
            return db.Posts.Where(p => p.Privacy == "Public" && p.Status == "Active")
                .Include("User")
                .OrderByDescending(p => p.PostId)
                .Skip(skip)
                .Take(take) 
                .ToList();
        }

        public string GetPostTitle(int postId)
        {
            //gets the title of a post
            return db.Posts.SingleOrDefault(s => s.PostId == postId).Title;
        }

        public List<Posts> GetFriendsPosts(Users user, int skip, int take)
        {
            var friendsPosts = new List<Posts>();
            var listFriendships = db.Friendship.Where(f => (f.UserId1 == user.UserId || f.UserId2 == user.UserId) && f.Status == 1).ToList();
            var friends = listFriendships.Select(a =>
            {
                //returns the friend
                if (a.UserId2 != user.UserId)
                {
                    return db.Users.Find(a.UserId2);
                    
                }
                return db.Users.Find(a.UserId1);
            }).ToList();
            //adds friends posts to list
            friends.ForEach(u =>
            {
                //gets all post for current friend
                var posts = db.Posts.Where(p => p.UserId == u.UserId && p.Status == "Active");
                //adds posts to list
                friendsPosts.AddRange(posts);
            });
            //orders the friends posts
            //and pages list
            var finalPosts = friendsPosts
                .OrderByDescending(p=>p.PostId)
                .Skip(skip)
                .Take(take)
                .ToList();
            return finalPosts;
        }

        public List<Posts> GetPostsByUserId(int UserId, int skip, int take)
        {
            return db.Posts
                .Where(p => p.UserId == UserId && p.Status == "Active")
                .Include("User")
                .OrderByDescending(p => p.PostId)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public bool DoesPostHaveProgram(int postid)
        {
            var count = db.PostItems.Where(p => p.ItemType == "Program" && p.PostId == postid).Count();
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesPostHaveSourceCode(int postid)
        {
            var count = db.PostItems.Where(p => p.ItemType == "SourceCode" && p.PostId == postid).Count();
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
