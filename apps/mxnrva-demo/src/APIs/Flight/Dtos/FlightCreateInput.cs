namespace MxnrvaDemo.APIs.Dtos;

public class FlightCreateInput
{
    public Aircraft? Aircraft { get; set; }

    public Airline? Airline { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public List<Booking>? Bookings { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DepartureTime { get; set; }

    public string? FlightNumber { get; set; }

    public string? Id { get; set; }

    public List<Seat>? Seats { get; set; }

    public DateTime UpdatedAt { get; set; }
}
