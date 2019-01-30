using EntityCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCore.Interfaces
{
    public interface iPost
    {
        List<Posts> GetPostsByUserId(int UserId,int skip, int take);
        List<Posts> GetFriendsPosts(Users user,int skip, int take);
        List<Posts> GetCommunityPosts(int skip,int take);
        bool DoesPostHaveProgram(int postid);
        bool DoesPostHaveSourceCode(int postid);
    }
}
