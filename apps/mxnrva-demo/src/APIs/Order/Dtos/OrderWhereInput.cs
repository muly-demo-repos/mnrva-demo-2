namespace MxnrvaDemo.APIs.Dtos;

public class OrderWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public List<string>? OrderItems { get; set; }

    public List<string>? Payments { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
