using System;

namespace PaintManagementSystem.Models;

public class Order
{
    public readonly DateTime CreatedAt;

    public PaintProduct Product { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public Order(PaintProduct product, int quantity){
        Product = product;
        Quantity = quantity;
        TotalPrice = quantity * product.Price;
        CreatedAt = DateTime.Now;
    }

    public String DisplayOrder(){
        return $"The product of order is {Product.DisplayInfo()}, the quantity of the product is {Quantity}, the order is created at {CreatedAt}.";
    }

    public String GetTotalPrice(){
        return $"The total price of the product is {TotalPrice}$";
    }
}
