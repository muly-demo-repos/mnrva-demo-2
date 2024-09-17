using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class BookingsService : BookingsServiceBase
{
    public BookingsService(MxnrvaDemoDbContext context)
        : base(context) { }
}
