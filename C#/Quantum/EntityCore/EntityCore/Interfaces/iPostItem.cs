using EntityCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCore.Interfaces
{
    public interface iPostItem
    {
        List<PostItems> GetPostPicturesByPostId(int PostId);
    }
}
