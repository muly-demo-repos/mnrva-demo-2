using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MxnrvaDemo.Infrastructure.Models;

[Table("Aircraft")]
public class AircraftDbModel
{
    public string? AirlineId { get; set; }

    [ForeignKey(nameof(AirlineId))]
    public AirlineDbModel? Airline { get; set; } = null;

    [Range(-999999999, 999999999)]
    public int? Capacity { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<FlightDbModel>? Flights { get; set; } = new List<FlightDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Model { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
