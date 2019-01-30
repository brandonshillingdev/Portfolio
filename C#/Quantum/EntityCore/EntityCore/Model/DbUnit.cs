using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityCore.Interfaces;
using EntityCore.Functions;


namespace EntityCore.Model
{
    public class DbUnit : IDisposable, iUnit
    {
        //vars
        private UserFunctions _user;
        private PostFunctions _post;
        private PostItemFunctions _postItem;
        private FriendshipFunctions _friend;

        private DbModel dbContext { get; set; }

        public DbUnit()
        {
            dbContext = new DbModel();
        }

        public UserFunctions Users
        {
            get
            {
                if (_user == null) _user = new UserFunctions(dbContext);

                return _user;
            }
        }

        public PostFunctions Posts
        {
            get
            {
                if (_post == null) _post = new PostFunctions(dbContext);

                return _post;
            }
        }

    public PostItemFunctions PostItem
        {
            get
            {
                if (_postItem == null) _postItem = new PostItemFunctions(dbContext);

                return _postItem;
            }
        }

        public FriendshipFunctions Friends
        {
            get
            {
                if (_friend == null) _friend = new FriendshipFunctions(dbContext);

                return _friend;
            }
        }

        public void Commit()
        {
            //commits database
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            //disposes database
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(dbContext != null)
                {
                    dbContext.Dispose();
                }
            }
        }
    }
}
