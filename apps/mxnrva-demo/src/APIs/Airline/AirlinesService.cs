using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class AirlinesService : AirlinesServiceBase
{
    public AirlinesService(MxnrvaDemoDbContext context)
        : base(context) { }
}
