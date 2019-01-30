using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCore.Functions;

namespace EntityCore.Interfaces
{
    public interface iUnit
    {
        void Commit();
        //gets function classes
        UserFunctions Users { get; }
        PostFunctions Posts { get; }
        PostItemFunctions PostItem { get; }
        FriendshipFunctions Friends { get; }
    }
}
