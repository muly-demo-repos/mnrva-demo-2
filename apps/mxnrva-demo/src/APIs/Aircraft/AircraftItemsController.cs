using Microsoft.AspNetCore.Mvc;

namespace MxnrvaDemo.APIs;

[ApiController()]
public class AircraftItemsController : AircraftItemsControllerBase
{
    public AircraftItemsController(IAircraftItemsService service)
        : base(service) { }
}
