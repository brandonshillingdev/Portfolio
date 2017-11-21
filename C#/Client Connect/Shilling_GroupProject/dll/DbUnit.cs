using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public class DbUnit : IDisposable, iUnit
    {
        //vars
        private UserFunctions _user;
        private PhotoFunctions _photo;
        private FinancialFunctions _financial;
        private DbModel dbContext { get; set; }

        public DbUnit()
        {
            dbContext = new DbModel();
        }

        public UserFunctions userfunction
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserFunctions(dbContext);
                }
                return _user;
            }
        }

        public PhotoFunctions photofunction
        {
            //gets and returns photo
            get
            {
                //if null get data
                if(_photo == null)
                {
                    _photo = new PhotoFunctions(dbContext);
                }
                return _photo;
            }
        }

        public FinancialFunctions financialfunction
        {
            get
            {
                if (_financial == null)
                {
                    _financial = new FinancialFunctions(dbContext);
                }
                return _financial;
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
