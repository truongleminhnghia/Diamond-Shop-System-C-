using Comom;
using DiamondShopSystem.Business.Base;
using DiamondShopSystem.Data;
using DiamondShopSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopSystem.Business
{
    public interface ICompanyBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Company company);
        Task<IBusinessResult> Update(Company company);
        Task<IBusinessResult> DeleteById(int id);

        Task<IBusinessResult> Search(string? name, string? address, string website, string? industry, bool? isActive);
    }
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public CompanyBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var company = await _unitOfWork.companyRepository.GetByIdAsync(id);
                if (company != null)
                {
                    var result = await _unitOfWork.companyRepository.RemoveAsync(company);
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

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var company = await _unitOfWork.companyRepository.GetAllAsync();
                if (company != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, company);
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
                var company = await _unitOfWork.companyRepository.GetByIdAsync(id);
                if (company == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, company);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Company company)
        {
            try
            {
                int result = await _unitOfWork.companyRepository.CreateAsync(company);
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

        public async Task<IBusinessResult> Search(string? name, string? address, string website, string? industry, bool? isActive)
        {
            try
            {
                IQueryable<Company> query = _unitOfWork.companyRepository.Query();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(c => c.CompanyName.Contains(name));
                }

                if (!string.IsNullOrEmpty(address))
                {
                    query = query.Where(c => c.Phone.Contains(address));
                }

                if (!string.IsNullOrEmpty(website))
                {
                    query = query.Where(c => c.Website.Contains(website));
                }

                if (!string.IsNullOrEmpty(industry))
                {
                    query = query.Where(c => c.Address.Contains(address));
                }

                if (isActive.HasValue)
                {
                    query = query.Where(c => c.IsActive == isActive);
                }

                var compary = await query.ToListAsync();

                if (compary.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, compary);
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

        public async Task<IBusinessResult> Update(Company company)
        {
            try
            {
                int result = await _unitOfWork.companyRepository.UpdateAsync(company);
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
    }
}
