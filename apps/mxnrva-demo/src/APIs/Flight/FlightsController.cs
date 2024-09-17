using Microsoft.AspNetCore.Mvc;

namespace MxnrvaDemo.APIs;

[ApiController()]
public class FlightsController : FlightsControllerBase
{
    public FlightsController(IFlightsService service)
        : base(service) { }
}
