namespace PaintStore.Model.Models;

public class Brand
{
    public int Id { get; set; }

    public string BrandName { get; set; } = null!;

    public Brand()
    {
    }

    public Brand(string brandName)
    {
        BrandName = brandName;
    }
}
