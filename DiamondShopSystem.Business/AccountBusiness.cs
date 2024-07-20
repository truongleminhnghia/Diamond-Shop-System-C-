using DiamondShopSystem.Data;
using DiamondShopSystem.Data.Models;
using DiamondShopSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopSystem.Business
{
    public class AccountBusiness
    {
        private readonly AccountRepository _repo;
        public AccountBusiness()
        {
            _repo ??= new AccountRepository();
        }

        //public Account Logins(string username, string password)
        //{
        //    return
        //}
    }
}
