using System;
using PaintManagementSystem.Enums;

namespace PaintManagementSystem.Models;

public class Payment
{
    public Order GetOrder { get; set; }

    public int paymentId { get; set; }

    public PaymentType paymentType { get; set; }

    public decimal paymentAmount { get; set; }

    public PaymentMethod Method { get; set; }

    public User user { get; set; }

    public DateTime CreatedAt { get; set; }

    public Payment(PaymentType paymenttype, PaymentMethod method, User username, int id, Order order, decimal amount)
    {
        paymentType = paymenttype;
        Method = method;
        user = username;
        GetOrder = order;
        paymentId = id;
        paymentAmount = amount;
        CreatedAt = DateTime.Now;

    }

    public string PaymentInfor(){
        return $"This payment is created for order{GetOrder.OrderId}: The type of payment is {paymentType}, the money is pay by {Method}, {paymentAmount}$ has be pay by {user.UserName}. This payment record is created at {CreatedAt} ";
    }
}
