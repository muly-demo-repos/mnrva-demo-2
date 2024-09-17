using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(MxnrvaDemoDbContext context)
        : base(context) { }
}
