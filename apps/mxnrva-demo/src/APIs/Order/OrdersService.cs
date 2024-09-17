using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(MxnrvaDemoDbContext context)
        : base(context) { }
}
