using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MxnrvaDemo.Infrastructure.Models;

[Table("Flights")]
public class FlightDbModel
{
    public string? AircraftId { get; set; }

    [ForeignKey(nameof(AircraftId))]
    public AircraftDbModel? Aircraft { get; set; } = null;

    public string? AirlineId { get; set; }

    [ForeignKey(nameof(AirlineId))]
    public AirlineDbModel? Airline { get; set; } = null;

    public DateTime? ArrivalTime { get; set; }

    public List<BookingDbModel>? Bookings { get; set; } = new List<BookingDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? DepartureTime { get; set; }

    [StringLength(1000)]
    public string? FlightNumber { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<SeatDbModel>? Seats { get; set; } = new List<SeatDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
