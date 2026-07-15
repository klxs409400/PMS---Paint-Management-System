using PaintStore.Model;

namespace PaintStore.Model.Models;

public class Payment
{
    public int Id { get; set; }

    public PaymentType Type { get; set; }

    public decimal Amount { get; set; }

    public PaymentMethod Method { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public int OrderId { get; set; }

    public Order Order { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public Payment()
    {
    }

    public Payment(int id, PaymentType type, PaymentMethod method, User user, Order order, decimal amount)
    {
        Id = id;
        Type = type;
        Method = method;
        User = user;
        Order = order;
        Amount = amount;
        CreatedDate = DateTime.Now;
    }

    public string PaymentInfor()
    {
        return $"This payment is created for order {Order.Id}: The type of payment is {Type}, the money is paid by {Method}, {Amount}$ has been paid by {User.Name}. This payment record is created at {CreatedDate}";
    }
}
