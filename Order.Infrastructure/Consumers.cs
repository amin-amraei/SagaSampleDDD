using Domain;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ReserveInventoryConsumer : IConsumer<IInventoryReserved>
    {
        public async Task Consume(ConsumeContext<IInventoryReserved> context)
        {
            Console.WriteLine($"Reserving inventory for order {context.Message.OrderId}");
            // Simulate success
            await context.Publish<IInventoryReserved>(new { OrderId = context.Message.OrderId });
        }
    }

    public class PaymentConsumer : IConsumer<IPaymentCompleted>
    {
        public async Task Consume(ConsumeContext<IPaymentCompleted> context)
        {
            Console.WriteLine($"Processing payment for order {context.Message.OrderId}");
            // Simulate success
            await context.Publish<IPaymentCompleted>(new { OrderId = context.Message.OrderId });
        }
    }
}
