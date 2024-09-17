using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class PassengersService : PassengersServiceBase
{
    public PassengersService(MxnrvaDemoDbContext context)
        : base(context) { }
}
