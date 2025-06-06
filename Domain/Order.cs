using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order
    {
        public Guid Id { get; private set; }
        public bool InventoryReserved { get; private set; }
        public bool PaymentCompleted { get; private set; }

        public Order(Guid id)
        {
            Id = id;
            InventoryReserved = false;
            PaymentCompleted = false;
        }

        public void MarkInventoryReserved() => InventoryReserved = true;
        public void MarkPaymentCompleted() => PaymentCompleted = true;
    }
}
