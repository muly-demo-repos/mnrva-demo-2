using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(MxnrvaDemoDbContext context)
        : base(context) { }
}
