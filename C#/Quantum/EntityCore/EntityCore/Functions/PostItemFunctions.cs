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
    public class PostItemFunctions : iPostItem
    {
        DbModel db;

        public PostItemFunctions(DbModel db)
        {
            this.db = db;
        }

        public Byte[] GetProgramInstallerByPostId(int id)
        {
            //gets program by post id
            return db.PostItems.SingleOrDefault(s => s.PostId == id && s.ItemType == "Program").Item;
        }
        public Byte[] GetSourceCodeByPostId(int id)
        {
            //gets program by post id
            return db.PostItems.SingleOrDefault(s => s.PostId == id && s.ItemType == "SourceCode").Item;
        }
        public List<PostItems> GetPostPicturesByPostId(int PostId)
        {
            //gets all post pictures for a post by the post id
            return db.PostItems.Where(p => p.PostId == PostId && p.ItemType == "Picture").ToList();
        }
    }
}
