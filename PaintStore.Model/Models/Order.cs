using PaintStore.Model;

namespace PaintStore.Model.Models;

public class Order
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<PaintsOrder> PaintsOrders { get; set; }

    public decimal totalPrice { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public Order()
    {
        PaintsOrders = new List<PaintsOrder>();

        // 初始化是为了防止 NullReferenceException：
        // 如果之后 EF 用 Include() 等方式查出了关联数据，会用真实数据覆盖掉这里的空 list；
        // 如果没有加载关联数据（比如忘了 Include，或者对象还没存库），这里就会保持空 list 而不是 null
    }

    public Order(int id, List<PaintsOrder> paintProducts, int userId)
    {
        Id = id;
        PaintsOrders = paintProducts;
        UserId = userId;
        CreatedDate = DateTime.Now;
    }


    public decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        for (int i = 0; i < PaintsOrders.Count; i++)
        {
            totalPrice += PaintsOrders[i].PaintProduct.Price * PaintsOrders[i].Quantity;
        }
        return totalPrice;
    }

    public PaintProduct? GetMostExpensivePaintProduct()
    {
        return PaintsOrders.Select(p => p.PaintProduct).OrderByDescending(p => p.Price).FirstOrDefault();
    }

    public void RemoveProduct(int productId)
    {
        PaintsOrders.RemoveAll(p => p.PaintProductId == productId);
    }

    public List<PaintProduct> SpecificPaint(decimal minPrice, decimal maxPrice)
    {
        return PaintsOrders.Select(p => p.PaintProduct).ToList().FindAll(p => p.Price > minPrice && p.Price < maxPrice);
    }

    public decimal FindTotalPrice(PaintType type)
    {
        return PaintsOrders.Select(p => p.PaintProduct).Where(p => p.Type == type).Select(p => p.Price).Sum();
    }

    public bool CheckIfHasNullName()
    {
        return PaintsOrders.Select(p => p.PaintProduct).Any(p => p.Name == null);
    }

    public string DisplayOrder()
    {
        string result = "";
        for (int i = 0; i < PaintsOrders.Count; i++)
        {
            result += $"The {i} product of order {Id} is {PaintsOrders[i].PaintProduct.DisplayInfo()} ";
        }
        result += $" it was created at {CreatedDate}";
        return result;
    }
}
