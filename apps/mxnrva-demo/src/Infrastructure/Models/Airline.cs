using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MxnrvaDemo.Infrastructure.Models;

[Table("Airlines")]
public class AirlineDbModel
{
    public List<AircraftDbModel>? AircraftItems { get; set; } = new List<AircraftDbModel>();

    [StringLength(1000)]
    public string? Country { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<FlightDbModel>? Flights { get; set; } = new List<FlightDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
