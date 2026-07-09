namespace PaintStore.Model;

public class User
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public List<Order> HistoricalOrder { get; set; }

    public List<Payment> HistoricalPayment { get; set; }

    public User()
    {
        HistoricalOrder = new List<Order>();
        HistoricalPayment = new List<Payment>();
    }

    public User(int id, string name, string email, string phone)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        CreatedDate = DateTime.Now;
        HistoricalOrder = new List<Order>();
        HistoricalPayment = new List<Payment>();
    }

    public string? GetMostExpensiveOrder()
    {
        Order? mostExpensive = HistoricalOrder.OrderByDescending(p => p.GetTotalPrice()).FirstOrDefault();
        return mostExpensive?.DisplayOrder();
    }

    public string? GetLatestOrder()
    {
        Order? latestOrder = HistoricalOrder.OrderByDescending(p => p.CreatedDate).FirstOrDefault();
        return latestOrder?.DisplayOrder();
    }

    public string? GetCheapestPayment()
    {
        Payment? cheapestPayment = HistoricalPayment.OrderBy(p => p.Amount).FirstOrDefault();
        return cheapestPayment?.PaymentInfor();
    }

    public string? GetLatestPayment()
    {
        Payment? latestPayment = HistoricalPayment.OrderByDescending(p => p.CreatedDate).FirstOrDefault();
        return latestPayment?.PaymentInfor();
    }

    public List<Payment> GetPaymentsAboveAmount(decimal amount)
    {
        return HistoricalPayment.FindAll(p => p.Amount > amount);
    }
}
