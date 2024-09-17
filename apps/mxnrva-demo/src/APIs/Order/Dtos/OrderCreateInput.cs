namespace MxnrvaDemo.APIs.Dtos;

public class OrderCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Id { get; set; }

    public List<OrderItem>? OrderItems { get; set; }

    public List<Payment>? Payments { get; set; }

    public string? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
