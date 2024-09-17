namespace MxnrvaDemo.APIs.Dtos;

public class Passenger
{
    public List<string>? Bookings { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string Id { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public DateTime UpdatedAt { get; set; }
}
