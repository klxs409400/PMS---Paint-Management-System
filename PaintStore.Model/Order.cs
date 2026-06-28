namespace PaintStore.Model;

public class Order
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<PaintProduct> PaintProducts { get; set; }

    public int UserId { get; set; }

    public Order(int id, List<PaintProduct> products, int userId)
    {
        Id = id;
        CreatedDate = DateTime.Now;
        PaintProducts = products;
        UserId = userId;
    }

    public Order()
    {
        PaintProducts = null!;
    }

    public decimal GetTotalPrice()
    {   decimal TotalPrice = 0;
        
        foreach(PaintProduct product in PaintProducts)
        {
            TotalPrice += product.Price;
        }

        return TotalPrice;
    }


}
