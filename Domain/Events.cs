namespace Domain
{
    public interface IOrderSubmitted
    {
        Guid OrderId { get; }
    }

    public interface IInventoryReserved
    {
        Guid OrderId { get; }
    }

    public interface IPaymentCompleted
    {
        Guid OrderId { get; }
    }

    public interface ICancelOrder
    {
        Guid OrderId { get; }
    }

    public class InventoryReservedEvent : IInventoryReserved
    {
        public Guid OrderId { get; set; }

        public InventoryReservedEvent(Guid orderId)
        {
            OrderId = orderId;
        }
        public InventoryReservedEvent() { }
    }
    public class PaymentCompleted : IPaymentCompleted
    {
        public Guid OrderId { get; set; }

        public PaymentCompleted(Guid orderId)
        {
            OrderId = orderId;
        }
        public PaymentCompleted()
        {
           
        }

    }

    public class SubmitOrder : IOrderSubmitted
    {
        public Guid OrderId { get; set; }

        public SubmitOrder(Guid orderId)
        {
            OrderId = orderId;
        }
        public SubmitOrder()
        {
            
        }

    }
    

}
