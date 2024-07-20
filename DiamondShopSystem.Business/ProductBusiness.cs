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
    public interface IProductBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(Product product);
        Task<IBusinessResult> Update(Product product);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> search(string? name, string? brand, string? diamond, string? price, string? size, bool? status);
    }
    public class ProductBusiness : IProductBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var product = await _unitOfWork.productRepository.GetAllAsync();
                if (product != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, product);
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
                var product = await _unitOfWork.productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, product);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Product product)
        {
            try
            {
                int result = await _unitOfWork.productRepository.CreateAsync(product);
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

        public async Task<IBusinessResult> Update(Product product)
        {
            try
            {
                int result = await _unitOfWork.productRepository.UpdateAsync(product);
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
                var product = await _unitOfWork.productRepository.GetByIdAsync(id);
                if (product != null)
                {
                    var result = await _unitOfWork.productRepository.RemoveAsync(product);
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

        public async Task<IBusinessResult> search(string? name, string? brand, string? diamond, string? price, string? size, bool? status)
        {
            try
            {
                IQueryable<Product> query = _unitOfWork.productRepository.Query();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(c => c.ProductName.Contains(name));
                }

                if (!string.IsNullOrEmpty(brand))
                {
                    query = query.Where(c => c.Brand.Contains(brand));
                }

                if (!string.IsNullOrEmpty(diamond))
                {
                    query = query.Where(c => c.Diamond.Contains(diamond));
                }

                if (!string.IsNullOrEmpty(price) && double.TryParse(price, out double parsedPrice))
                {
                    query = query.Where(c => c.Price == parsedPrice);
                }

                if (!string.IsNullOrEmpty(size) && int.TryParse(size, out int parsedSize))
                {
                    query = query.Where(c => c.Size == parsedSize);
                }

                if (status.HasValue)
                {
                    query = query.Where(c => c.Status == status.Value);
                }

                var prduct = await query.ToListAsync();

                if (prduct.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, prduct);
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
