using MxnrvaDemo.Core.Enums;

namespace MxnrvaDemo.APIs.Dtos;

public class BookingCreateInput
{
    public DateTime? BookingDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public Flight? Flight { get; set; }

    public string? Id { get; set; }

    public Passenger? Passenger { get; set; }

    public List<Seat>? Seats { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
