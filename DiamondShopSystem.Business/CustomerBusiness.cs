using Comom;
using DiamondShopSystem.Business.Base;
using DiamondShopSystem.Data;
using DiamondShopSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DiamondShopSystem.Business
{
    public interface ICustomerBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Customer customer);
        Task<IBusinessResult> Update(Customer customer);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> SearchCustomers(string? name, string? phone, string? gender, string? address, string? email, bool? isActive);
    }
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var customers = await _unitOfWork.customerRepository.GetAllAsync();
                if (customers != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customers);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var customers = await _unitOfWork.customerRepository.GetByIdAsync(id);
                if (customers == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customers);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Customer customer)
        {
            try
            {
                int result = await _unitOfWork.customerRepository.CreateAsync(customer);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Customer customer)
        {
            try
            {
                int result = await _unitOfWork.customerRepository.UpdateAsync(customer);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var customer = await _unitOfWork.customerRepository.GetByIdAsync(id);
                if (customer != null)
                {
                    var result = await _unitOfWork.customerRepository.RemoveAsync(customer);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }


        public async Task<IBusinessResult> SearchCustomers(string? name, string? phone, string? gender, string? address, string? email, bool? isActive)
        {
            try
            {
                IQueryable<Customer> query = _unitOfWork.customerRepository.Query();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(c => c.CustomerName.Contains(name));
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    query = query.Where(c => c.Phone.Contains(phone));
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    query = query.Where(c => c.Gender.Equals(gender));
                }

                if (!string.IsNullOrEmpty(address))
                {
                    query = query.Where(c => c.Address.Contains(address));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(c => c.Email.Contains(email));
                }

                if (isActive.HasValue)
                {
                    query = query.Where(c => c.IsActive == isActive.Value);
                }

                var customers = await query.ToListAsync();

                if (customers.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customers);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.FAIL_READ_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
