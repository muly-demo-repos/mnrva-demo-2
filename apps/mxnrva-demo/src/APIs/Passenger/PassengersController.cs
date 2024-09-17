using Microsoft.AspNetCore.Mvc;

namespace MxnrvaDemo.APIs;

[ApiController()]
public class PassengersController : PassengersControllerBase
{
    public PassengersController(IPassengersService service)
        : base(service) { }
}
