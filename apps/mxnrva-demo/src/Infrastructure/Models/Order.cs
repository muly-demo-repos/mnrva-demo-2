using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MxnrvaDemo.Infrastructure.Models;

[Table("Orders")]
public class OrderDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<OrderItemDbModel>? OrderItems { get; set; } = new List<OrderItemDbModel>();

    [StringLength(256)]
    public string? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
