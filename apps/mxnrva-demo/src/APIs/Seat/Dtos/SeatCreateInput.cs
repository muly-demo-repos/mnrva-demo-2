namespace MxnrvaDemo.APIs.Dtos;

public class SeatCreateInput
{
    public Booking? Booking { get; set; }

    public DateTime CreatedAt { get; set; }

    public Flight? Flight { get; set; }

    public string? Id { get; set; }

    public string? NumberField { get; set; }

    public DateTime UpdatedAt { get; set; }
}
