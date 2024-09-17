namespace MxnrvaDemo.APIs.Dtos;

public class OrderUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public List<string>? OrderItems { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }
}