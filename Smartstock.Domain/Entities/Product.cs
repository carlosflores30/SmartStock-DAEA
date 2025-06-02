namespace Smartstock.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int Threshold { get; set; }
    public Guid? Categoryid { get; set; }
    public DateTime? Createdat { get; set; }
}