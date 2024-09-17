using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class OrderItemsService : OrderItemsServiceBase
{
    public OrderItemsService(MxnrvaDemoDbContext context)
        : base(context) { }
}
