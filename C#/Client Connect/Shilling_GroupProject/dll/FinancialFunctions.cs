using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DbOps2
{
    public class FinancialFunctions : RepositoryClass<Financial>, iFinancial
    {
        public FinancialFunctions(DbContext dbcontext) : base(dbcontext)
        {
        }

        public List<Financial> getAllFinancialForAdmin(int id)
        {
            //gets financial information for admins
            return GetALL().Where(f => f.AdminId == id).ToList();
        }

        public List<Financial> getAllFinancialForPhotographer(int id)
        {
            //gets financial information for photographers
            return GetALL().Where(f => f.PhotographerId == id).ToList();
        }

        public List<Financial> getSingleFinancial(int id)
        {
            //gets all financial information for a single client
            return GetALL().Where(f => f.ClientId == id).ToList();
        }

        public List<Financial> getSingleFinancialUnpaid(int id)
        {
            //gets all unpaid financials for single client
            return GetALL().Where(f => f.ClientId == id && f.Paid == "unpaid").ToList();
        }
    }
}
