namespace PaintStore.Model;

public class PaintStore
{
    public int Id { get; set; }

    public List<PaintProduct> Products { get; set; }

    public PaintStore()
    {
        Products = new List<PaintProduct>();
    }

    public PaintStore(List<PaintProduct> products)
    {
        Products = products;
    }

    public string PaintInfor()
    {
        string information = "";
        foreach (PaintProduct product in Products)
        {
            information += product.DisplayInfo();
        }
        return information;
    }
}
