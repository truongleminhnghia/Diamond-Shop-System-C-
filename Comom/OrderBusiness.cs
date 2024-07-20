//using Comom;

//namespace DiamondShopSystem.Business
//{

//    public interface IOrderBusiness
//    {
//        Task<IBusinessResult> GetAll();
//        Task<IBusinessResult> GetById(int code);
//        Task<IBusinessResult> Save(Order order);
//        Task<IBusinessResult> Update(Order order);
//        Task<IBusinessResult> DeleteById(int code);
//        Task<IBusinessResult> SearchOrder(DateTime? orderDay, string? OrderStatus, string? PaymentMethod, string? PaymentStatus, string? ShippingAddress, string? Discount, string? Note);
//    }

//    public class OrderBusiness : IOrderBusiness
//    {

//        private readonly UnitOfWork _unitOfWork;

//        public OrderBusiness()
//        {
//            _unitOfWork ??= new UnitOfWork();
//        }

//        public async Task<IBusinessResult> GetAll()
//        {
//            try
//            {
//                var order = await _unitOfWork.orderRepository.GetAllAsync();
//                if (order == null)
//                {
//                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
//                }
//                else
//                {
//                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
//            }
//        }
//        public async Task<IBusinessResult> GetById(int code)
//        {
//            try
//            {
//                var order = await _unitOfWork.orderRepository.GetByIdAsync(code);
//                if (order == null)
//                {
//                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
//                }
//                else
//                {
//                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
//            }
//        }
//        public async Task<IBusinessResult> Save(Order order)
//        {
//            try
//            {
//                //int result = await _currencyRepository.CreateAsync(currency);
//                int result = await _unitOfWork.orderRepository.CreateAsync(order);
//                if (result > 0)
//                {
//                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
//                }
//                else
//                {
//                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
//            }
//        }
//        public async Task<IBusinessResult> Update(Order order)
//        {
//            try
//            {
//                //UnitOfWork.UpdateAsync(order);
//                int result = await _unitOfWork.orderRepository.UpdateAsync(order);

//                if (result > 0)
//                {
//                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
//                }
//                else
//                {
//                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(-4, ex.ToString());
//            }
//        }
//        public async Task<IBusinessResult> DeleteById(int code)
//        {
//            try
//            {
//                //var currency = await _currencyRepository.GetByIdAsync(code);
//                var currency = await _unitOfWork.orderRepository.GetByIdAsync(code);
//                if (currency != null)
//                {
//                    //var result = await _currencyRepository.RemoveAsync(currency);
//                    var result = await _unitOfWork.orderRepository.RemoveAsync(currency);
//                    if (result)
//                    {
//                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
//                    }
//                    else
//                    {
//                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
//                    }
//                }
//                else
//                {
//                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(-4, ex.ToString());
//            }
//        }

//        public async Task<IBusinessResult> SearchOrder(DateTime? orderDay, string? OrderStatus, string? PaymentMethod, string? PaymentStatus, string? ShippingAddress, string? Discount, string? Note)
//        {
//            try
//            {
//                IQueryable<Order> query = _unitOfWork.orderRepository.Query();
//                if (!string.IsNullOrEmpty(orderDay.ToString()))
//                {
//                    query = query.Where(c => c.OrderDay.Equals(orderDay));
//                }
//                if (!string.IsNullOrEmpty(OrderStatus))
//                {
//                    query = query.Where(c => c.OrderStatus.Contains(OrderStatus));
//                }

//                if (!string.IsNullOrEmpty(PaymentMethod))
//                {
//                    query = query.Where(c => c.PaymentMethod.Contains(PaymentMethod));
//                }
//                if (!string.IsNullOrEmpty(PaymentStatus))
//                {
//                    query = query.Where(c => c.PaymentStatus.Equals(PaymentStatus));
//                }

//                if (!string.IsNullOrEmpty(ShippingAddress))
//                {
//                    query = query.Where(c => c.ShippingAddress.Contains(ShippingAddress));
//                }

//                if (!string.IsNullOrEmpty(Discount))
//                {
//                    query = query.Where(c => c.Discount.Contains(Discount));
//                }

//                if (!string.IsNullOrEmpty(Note))
//                {
//                    query = query.Where(c => c.Note.Contains(Note));
//                }

//                var order = await query.ToListAsync();

//                if (order.Count > 0)
//                {
//                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order);
//                }
//                else
//                {
//                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
//                }
//            }
//            catch (Exception ex)
//            {
//                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
//            }
//        }
//    }
//}
