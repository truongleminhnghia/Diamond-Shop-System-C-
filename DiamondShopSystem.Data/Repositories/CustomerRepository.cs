using DiamondShopSystem.Data.Base;
using DiamondShopSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopSystem.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository() { }

        public CustomerRepository(Net1804_212_1_DiamondShopSystemV3Context context) => _context = context;
    }
}
