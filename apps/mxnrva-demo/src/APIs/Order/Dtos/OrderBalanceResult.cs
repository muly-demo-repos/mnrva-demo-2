using System.ComponentModel.DataAnnotations;

namespace MxnrvaDemo.APIs;

public class OrderBalanceResult
{
    [Required()]
    public double OrderBalance { get; set; }
}
