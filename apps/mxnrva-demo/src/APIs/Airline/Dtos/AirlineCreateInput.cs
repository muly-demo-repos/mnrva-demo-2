namespace MxnrvaDemo.APIs.Dtos;

public class AirlineCreateInput
{
    public List<Aircraft>? AircraftItems { get; set; }

    public string? Country { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<Flight>? Flights { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
