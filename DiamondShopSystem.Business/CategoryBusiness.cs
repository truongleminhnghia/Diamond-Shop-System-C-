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
    public interface ICategoryBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Category category);
        Task<IBusinessResult> Update(Category category);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> GetByName(string name);
        Task<Category?> GetCategory(int id);
        Task<IEnumerable<Category>> GetCategories();
    }
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var category = await _unitOfWork.categoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    var result = await _unitOfWork.categoryRepository.RemoveAsync(category);
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
                var category = await _unitOfWork.categoryRepository.GetAllAsync();
                if (category != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, category);
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
                var category = await _unitOfWork.categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, category);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetByName(string name)
        {
            try
            {
                IQueryable<Category> query = _unitOfWork.categoryRepository.Query();
                if(!string.IsNullOrEmpty(name))
                {
                    query = query.Where(c => c.CategoryName.Contains(name));
                }
                var result = await query.ToListAsync();
                if(result.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
                } else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.FAIL_READ_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var categories = await _unitOfWork.categoryRepository.GetAllAsync();
                if(categories != null)
                {
                    return categories;
                } else
                {
                    return Enumerable.Empty<Category>();
                }
            } catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving categories.", ex);
            }
        }

        public async Task<Category?> GetCategory(int id)
        {
            try
            {
                var category = await _unitOfWork.categoryRepository.GetByIdAsync(id);
                return category;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the category.", ex);
            }
        }

        public async Task<IBusinessResult> Save(Category category)
        {
            try
            {
                int result = await _unitOfWork.categoryRepository.CreateAsync(category);
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

        public async Task<IBusinessResult> Update(Category category)
        {
            try
            {
                int result = await _unitOfWork.categoryRepository.UpdateAsync(category);
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
