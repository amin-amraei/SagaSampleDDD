using Domain;
using MassTransit;

namespace Application
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderService(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task SubmitOrderAsync(Guid orderId)
        {
            var order = new Order(orderId);
            await _orderRepository.SaveOrderAsync(order);

            await _publishEndpoint.Publish<IOrderSubmitted>(new { OrderId = orderId });
        }
    }
}
