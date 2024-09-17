using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MxnrvaDemo.Infrastructure.Models;

[Table("Seats")]
public class SeatDbModel
{
    public string? BookingId { get; set; }

    [ForeignKey(nameof(BookingId))]
    public BookingDbModel? Booking { get; set; } = null;

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? FlightId { get; set; }

    [ForeignKey(nameof(FlightId))]
    public FlightDbModel? Flight { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? NumberField { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
