using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class SeatsService : SeatsServiceBase
{
    public SeatsService(MxnrvaDemoDbContext context)
        : base(context) { }
}
