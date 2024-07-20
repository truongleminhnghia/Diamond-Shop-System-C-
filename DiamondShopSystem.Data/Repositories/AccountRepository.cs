using DiamondShopSystem.Data.Base;
using DiamondShopSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopSystem.Data.Repositories
{
    public class AccountRepository 
    {
        public readonly Net1804_212_1_DiamondShopSystemV3Context _context;
        
        public AccountRepository() 
        {
            _context ??= new Net1804_212_1_DiamondShopSystemV3Context();
        }
        public Account Login (string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.UseName == username);
        }
    }
}
