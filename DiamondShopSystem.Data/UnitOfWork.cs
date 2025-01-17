﻿using DiamondShopSystem.Data.Models;
using DiamondShopSystem.Data.Repositories;

namespace DiamondShopSystem.Data
{
    public class UnitOfWork
    {
        private Net1804_212_1_DiamondShopSystemV3Context _unitOfWorkContext;
        private ProductRepository _product;
        private CompanyRepository _company;
        private CustomerRepository _customer;
        private CategoryRepository _category;
        private OrderRepository _order;
        private OrderDetailRepository _orderDetail;
        


        public ProductRepository productRepository
        {
            get
            {
                return _product ??= new Repositories.ProductRepository();
            }
        }

        

        public CategoryRepository categoryRepository
        {
            get
            {
                return _category ??= new Repositories.CategoryRepository();
            }
        }


        public CompanyRepository companyRepository
        {
            get
            {
                return _company ??= new Repositories.CompanyRepository();
            }
        }

        public CustomerRepository customerRepository
        {
            get
            {
                return _customer ??= new Repositories.CustomerRepository();
            }
        }

        public OrderRepository orderRepository
        {
            get
            {
                return _order ??= new Repositories.OrderRepository();
            }
        }

        public OrderDetailRepository orderDetailRepository
        {
            get
            {
                return _orderDetail ??= new Repositories.OrderDetailRepository();
            }
        }

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}
