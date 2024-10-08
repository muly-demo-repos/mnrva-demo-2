namespace MxnrvaDemo.APIs.Dtos;

public class OrderItemUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Order { get; set; }

    public double? Price { get; set; }

    public string? Sku { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
