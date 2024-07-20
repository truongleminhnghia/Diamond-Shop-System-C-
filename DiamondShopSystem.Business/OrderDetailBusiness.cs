using Comom;
using DiamondShopSystem.Business.Base;
using DiamondShopSystem.Data;
using DiamondShopSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopSystem.Business
{
    public interface IOrderDetailBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int id);
        Task<IBusinessResult> Save(OrderDetail orderDetail);
        Task<IBusinessResult> Update(OrderDetail orderDetail);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> SearchOrderDetail(string? productId, string? orderId, string? quantity);

    }
    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderDetailBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var orderDetails = await _unitOfWork.orderDetailRepository.GetAllAsync();
                if (orderDetails != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);
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
                var orderDetail = await _unitOfWork.orderDetailRepository.GetByIdAsync(id);
                if (orderDetail == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.orderDetailRepository.CreateAsync(orderDetail);
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

        public async Task<IBusinessResult> Update(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.orderDetailRepository.UpdateAsync(orderDetail);
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
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                var orderDetail = await _unitOfWork.orderDetailRepository.GetByIdAsync(id);
                if (orderDetail != null)
                {
                    var result = await _unitOfWork.orderDetailRepository.RemoveAsync(orderDetail);
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
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> SearchOrderDetail(string? productId, string? orderId, string? quantity)
        {
            try
            {
                IQueryable<OrderDetail> query = _unitOfWork.orderDetailRepository.Query();

                if (!string.IsNullOrEmpty(productId))
                {
                    query = query.Where(c => c.ProductId.ToString().Contains(productId));
                }

                if (!string.IsNullOrEmpty(orderId))
                {
                    query = query.Where(c => c.OrderId.ToString().Contains(orderId));
                }

                if (!string.IsNullOrEmpty(quantity))
                {
                    query = query.Where(c => c.Quantity.ToString().Contains(quantity));
                }

                var orderDetails = await query.ToListAsync();

                if (orderDetails.Count > 0)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);
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
