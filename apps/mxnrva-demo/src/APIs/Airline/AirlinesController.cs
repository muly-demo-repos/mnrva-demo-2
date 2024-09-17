using Microsoft.AspNetCore.Mvc;

namespace MxnrvaDemo.APIs;

[ApiController()]
public class AirlinesController : AirlinesControllerBase
{
    public AirlinesController(IAirlinesService service)
        : base(service) { }
}
