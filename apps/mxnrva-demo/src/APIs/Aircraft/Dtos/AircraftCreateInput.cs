namespace MxnrvaDemo.APIs.Dtos;

public class AircraftCreateInput
{
    public Airline? Airline { get; set; }

    public int? Capacity { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<Flight>? Flights { get; set; }

    public string? Id { get; set; }

    public string? Model { get; set; }

    public DateTime UpdatedAt { get; set; }
}
