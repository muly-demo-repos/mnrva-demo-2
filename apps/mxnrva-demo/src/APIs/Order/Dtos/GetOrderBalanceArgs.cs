using System.ComponentModel.DataAnnotations;

namespace MxnrvaDemo.APIs;

public class GetOrderBalanceArgs
{
    [Required()]
    public string OrderId { get; set; }
}
