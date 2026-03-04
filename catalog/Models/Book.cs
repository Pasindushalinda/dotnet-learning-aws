namespace catalog.Models;

public class Book
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Pages { get; set; }
    public string Author { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    public int InStock { get; set; }
}