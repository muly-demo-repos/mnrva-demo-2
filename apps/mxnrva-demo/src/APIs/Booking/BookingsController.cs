using Microsoft.AspNetCore.Mvc;

namespace MxnrvaDemo.APIs;

[ApiController()]
public class BookingsController : BookingsControllerBase
{
    public BookingsController(IBookingsService service)
        : base(service) { }
}
