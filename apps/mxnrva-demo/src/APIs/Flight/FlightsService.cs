using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class FlightsService : FlightsServiceBase
{
    public FlightsService(MxnrvaDemoDbContext context)
        : base(context) { }
}
