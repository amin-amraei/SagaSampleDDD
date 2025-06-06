using Domain;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderSagaDbContext _context;

        public OrderRepository(OrderSagaDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderAsync(Guid id)
        {
            return await _context.Set<Order>().FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task SaveOrderAsync(Order order)
        {
            var existing = await _context.Set<Order>().FindAsync(order.Id);
            if (existing == null)
                _context.Set<Order>().Add(order);
            else
                _context.Entry(existing).CurrentValues.SetValues(order);

            await _context.SaveChangesAsync();
        }
    }
}
