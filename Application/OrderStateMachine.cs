using Domain;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = default!;
        public Guid OrderId { get; set; }
        public bool InventoryReserved { get; set; }
        public bool PaymentCompleted { get; set; }
    }

    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public State Submitted { get; private set; }
        public State InventoryReservedState { get; private set; }
        public State PaymentCompletedState { get; private set; }
        public State Cancelled { get; private set; }

        public Event<IOrderSubmitted> OrderSubmitted { get; private set; }
        public Event<IInventoryReserved> InventoryReserved { get; private set; }
        public Event<IPaymentCompleted> PaymentCompleted { get; private set; }
        public Event<ICancelOrder> CancelOrder { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderSubmitted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => InventoryReserved, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PaymentCompleted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => CancelOrder, x => x.CorrelateById(m => m.Message.OrderId));

            Initially(
                When(OrderSubmitted)
                    .Then(context =>
                    {
                        context.Instance.OrderId = context.Data.OrderId;
                        Console.WriteLine($"Order submitted: {context.Data.OrderId}");
                    })
                    .TransitionTo(Submitted)
                    .Publish(context => new InventoryReservedEvent(context.Data.OrderId))

            );

            During(Submitted,
                When(InventoryReserved)
                    .Then(context =>
                    {
                        context.Instance.InventoryReserved = true;
                        Console.WriteLine($"Inventory reserved for order {context.Data.OrderId}");
                    })
                    .TransitionTo(InventoryReservedState)
                    .Publish(context => new PaymentCompleted(context.Data.OrderId))
            );

            During(InventoryReservedState,
                When(PaymentCompleted)
                    .Then(context =>
                    {
                        context.Instance.PaymentCompleted = true;
                        Console.WriteLine($"Payment completed for order {context.Data.OrderId}");
                    })
                    .TransitionTo(PaymentCompletedState)
                    .Finalize()
            );

            DuringAny(
                When(CancelOrder)
                    .Then(context =>
                    {
                        Console.WriteLine($"Order cancelled: {context.Data.OrderId}");
                    })
                    .TransitionTo(Cancelled)
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}
