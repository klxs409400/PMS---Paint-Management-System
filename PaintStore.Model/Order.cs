namespace PaintStore.Model;

public class Order
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<PaintProduct> PaintProducts { get; set; }

    public int[] Quantities { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public Order()
    {
        PaintProducts = new List<PaintProduct>();
        Quantities = Array.Empty<int>();
        // 初始化是为了防止 NullReferenceException：
        // 如果之后 EF 用 Include() 等方式查出了关联数据，会用真实数据覆盖掉这里的空 list；
        // 如果没有加载关联数据（比如忘了 Include，或者对象还没存库），这里就会保持空 list 而不是 null
    }

    public Order(int id, List<PaintProduct> paintProducts, int[] quantities, int userId)
    {
        Id = id;
        PaintProducts = paintProducts;
        Quantities = quantities;
        UserId = userId;
        CreatedDate = DateTime.Now;
    }

    public int GetTotalQuantity()
    {
        return Quantities.Sum();
    }

    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        for (int i = 0; i < PaintProducts.Count; i++)
        {
            totalPrice += PaintProducts[i].Price * Quantities[i];
        }
        return totalPrice;
    }

    public PaintProduct? GetMostExpensivePaintProduct()
    {
        return PaintProducts.OrderByDescending(p => p.Price).FirstOrDefault();
    }

    public void RemoveProduct(int productId)
    {
        PaintProducts.RemoveAll(p => p.Id == productId);
    }

    public List<PaintProduct> SpecificPaint(decimal minPrice, decimal maxPrice)
    {
        return PaintProducts.FindAll(p => p.Price > minPrice && p.Price < maxPrice);
    }

    public decimal FindTotalPrice(PaintType type)
    {
        return PaintProducts.Where(p => p.Type == type).Select(p => p.Price).Sum();
    }

    public bool CheckIfHasNullName()
    {
        return PaintProducts.Any(p => p.Name == null);
    }

    public string DisplayOrder()
    {
        string result = "";
        for (int i = 0; i < PaintProducts.Count; i++)
        {
            result += $"The {i} product of order {Id} is {PaintProducts[i].DisplayInfo()}, the quantity of this product is {Quantities[i]} ";
        }
        result += $"The total quantity of order {Id} is {GetTotalQuantity()}, and it was created at {CreatedDate}";
        return result;
    }
}
