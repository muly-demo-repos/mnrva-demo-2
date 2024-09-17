namespace MxnrvaDemo.APIs.Dtos;

public class Aircraft
{
    public string? Airline { get; set; }

    public int? Capacity { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<string>? Flights { get; set; }

    public string Id { get; set; }

    public string? Model { get; set; }

    public DateTime UpdatedAt { get; set; }
}
