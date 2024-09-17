using MxnrvaDemo.Infrastructure;

namespace MxnrvaDemo.APIs;

public class AircraftItemsService : AircraftItemsServiceBase
{
    public AircraftItemsService(MxnrvaDemoDbContext context)
        : base(context) { }
}
