using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MxnrvaDemo.Infrastructure.Models;

[Table("Payments")]
public class PaymentDbModel
{
    [Range(0, 99999999999)]
    public double? Amount { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public OrderDbModel? Order { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
