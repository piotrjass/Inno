namespace InnoProducts.Models;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool Availability { get; set; }
    public string CreatorUserID { get; set; }
    public DateTime CreationDate { get; set; }
}