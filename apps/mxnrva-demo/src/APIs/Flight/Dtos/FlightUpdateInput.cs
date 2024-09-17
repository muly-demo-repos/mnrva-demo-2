namespace MxnrvaDemo.APIs.Dtos;

public class FlightUpdateInput
{
    public string? Aircraft { get; set; }

    public string? Airline { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public List<string>? Bookings { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? DepartureTime { get; set; }

    public string? FlightNumber { get; set; }

    public string? Id { get; set; }

    public List<string>? Seats { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
