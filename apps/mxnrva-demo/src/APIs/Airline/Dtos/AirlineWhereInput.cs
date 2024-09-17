namespace MxnrvaDemo.APIs.Dtos;

public class AirlineWhereInput
{
    public List<string>? AircraftItems { get; set; }

    public string? Country { get; set; }

    public DateTime? CreatedAt { get; set; }

    public List<string>? Flights { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
