using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderAsync(Guid id);
        Task SaveOrderAsync(Order order);
    }
}
