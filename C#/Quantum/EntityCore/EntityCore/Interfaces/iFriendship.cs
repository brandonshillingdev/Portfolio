using EntityCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCore.Interfaces
{
    public interface iFriendship
    {
        List<Friendship> GetFriends(int CurrentUserId);
        List<Friendship> GetPendingFriendRequests(int CurrentUserId);
        List<Friendship> GetPendingFriendRequestsOut(int CurrentUserId);
        Friendship GetFriendship(int userid1, int userid2);
    }
}
