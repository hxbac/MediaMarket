﻿using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Order;
using MediaMarket.Application.DTO.Request.Product;
using MediaMarket.Application.DTO.Response.Order;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Services
{
    public class OrderService(
        IPaymentService paymentService,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IProductDetailRepository productDetailRepository,
        IBalanceService balanceService,
        IUser user
    ) : BaseResponseHandler, IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IProductDetailRepository _productDetailRepository = productDetailRepository;
        private readonly IBalanceService _balanceService = balanceService;
        private readonly IUser _user = user;

        public async Task<BaseResponse<CallbackStripeResponse>> CallbackStripe(CallbackStripeRequest request)
        {
            var order = await _orderRepository
                .FindAsync(x => x.PaymentSession == request.SessionId);

            var isSuccess = await _paymentService.IsPaymentSuccess(request.SessionId);
            if (isSuccess && order.Status == Domain.Enums.OrderStatus.Pending)
            {
                await FulFillOrderSuccess(order);

                var product = await _productRepository.FindByIdAsync(order.ProductId);
                await _balanceService.UpdateUserBalance(
                    product.CreatedBy,
                    order.Price,
                    order.Id,
                    Domain.Enums.TransactionType.Sell,
                    Domain.Enums.BalanceType.TopUp
                );
            }

            return Success(new CallbackStripeResponse()
            {
                IsSuccess = isSuccess,
                Order = order
            });
        }

        private async Task<Order> FulFillOrderSuccess(Order order)
        {
            order.Status = Domain.Enums.OrderStatus.Completed;
            await _orderRepository.SaveChangesAsync();
            return order;
        }

        public async Task<BaseResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest request)
        {
            var product = await _productRepository.GetProductWithLatestVersion(request.ProductSlug);

            var orderPrice = product.Price;
            if (product.Discounts != null && product.Discounts.Count > 0)
            {
                foreach (var item in product.Discounts)
                {
                    switch (item.Type)
                    {
                        case Domain.Enums.DiscountType.Fixed:
                            orderPrice -= (long)item.Value;
                            break;
                        case Domain.Enums.DiscountType.Percent:
                            orderPrice -= (long)(product.Price * item.Value / 100);
                            break;
                    }
                }
            }
            product.Price = orderPrice;

            var order = new Order()
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                ProductName = product.Name,
                Price = orderPrice,
                Status = Domain.Enums.OrderStatus.Pending,
                ProductVersion = product.Version,
                PaymentId = Guid.NewGuid(),
                PaymentMethod = "Stripe"
            };
            var payment = await _paymentService.Create(product);

            order.PaymentSession = payment.PaymentSession;

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return Success(new CreateOrderResponse()
            {
                RedirectUrl = payment.RedirectUrl,
            });
        }

        public async Task<BaseResponse<PaginatedResult<ProductPurchaseResponse>>> GetMyPurchasesPaginated(GetProductListRequest request)
        {
            var result = await _orderRepository.GetListPurchasesForUser(_user.Id, request.ProductType, request.Name, request.Page, request.PageSize);
            return Success(result);
        }

        public async Task<BaseResponse<PaginatedResult<ProductOrderResponse>>> GetMyOrdersPaginated(GetProductListRequest request)
        {
            var result = await _orderRepository.GetListOrdersForUser(_user.Id, request.ProductType, request.Name, request.Page, request.PageSize);
            return Success(result);
        }

        public async Task<BaseResponse<object>> DownloadProductOrder(Guid id)
        {
            var order = await _orderRepository.FindByIdAsync(id);
            var productDetail = await _productDetailRepository.FindAsync(x => x.ProductId == order.ProductId && x.Version == order.ProductVersion);

            return Success<object>((new { Url = productDetail.FileUrl }));
        }
    }
}
