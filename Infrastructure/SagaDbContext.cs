using Application;
using Domain;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure
{
    public class OrderSagaDbContext : SagaDbContext
    {
        public DbSet<Order> Order { get; set; }
        public OrderSagaDbContext(DbContextOptions<OrderSagaDbContext> options)
            : base(options)
        {
        }

        // مهم‌ترین بخش برای مپ کردن Saga State به EF Core
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new OrderStateMap();
            }
        }
    }
    public class OrderStateMap : SagaClassMap<OrderState>
    {
        protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState);
            entity.Property(x => x.OrderId);
            // هر property دیگری از OrderState که باید ذخیره شود را اضافه کن
        }
    }

}
