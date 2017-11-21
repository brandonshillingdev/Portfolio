using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public interface iFinancial : iRepository<Financial>
    {
        List<Financial> getAllFinancialForPhotographer(int id);
        List<Financial> getSingleFinancial(int id);
        List<Financial> getAllFinancialForAdmin(int id);
        List<Financial> getSingleFinancialUnpaid(int id);
    }
}
