﻿using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Order;
using MediaMarket.Application.DTO.Response.Order;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Services
{
    public class OrderService(
        IPaymentService paymentService,
        IOrderRepository orderRepository,
        IProductRepository productRepository
    ) : BaseResponseHandler, IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<BaseResponse<CallbackStripeResponse>> CallbackStripe(CallbackStripeRequest request)
        {
            var order = await _orderRepository
                .FindAsync(x => x.PaymentSession == request.SessionId);

            var isSuccess = _paymentService.IsPaymentSuccess(request.SessionId);
            if (isSuccess && order.Status == Domain.Enums.OrderStatus.Pending)
            {
                await FulFillOrderSuccess(order);
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

        public async Task<BaseResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest request, Guid userId)
        {
            var product = await _productRepository.GetProductWithLatestVersion(request.ProductSlug);
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Status = Domain.Enums.OrderStatus.Pending,
                ProductVersion = product.Version,
                PaymentId = Guid.NewGuid(),
                PaymentMethod = "Stripe"
            };
            var payment = _paymentService.Create(product);

            order.PaymentSession = payment.PaymentSession;

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return Success(new CreateOrderResponse()
            {
                RedirectUrl = payment.RedirectUrl,
            });
        }
    }
}