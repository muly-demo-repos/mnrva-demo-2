using Microsoft.AspNetCore.Mvc;

namespace MxnrvaDemo.APIs;

[ApiController()]
public class SeatsController : SeatsControllerBase
{
    public SeatsController(ISeatsService service)
        : base(service) { }
}
