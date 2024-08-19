namespace InspireMind.Education.MVC;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

}
