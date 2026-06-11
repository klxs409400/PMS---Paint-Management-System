using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Xml.XPath;

namespace PaintManagementSystem.Models;

public class Order
{
    public readonly DateTime CreatedAt;

    public PaintProduct[] Products { get; set; }

    public int[] Quantities { get; set; }

    public int TotalQuantity {get; set;}

    public decimal TotalPrice { get; set; }

    public Order(PaintProduct[] products, int[] quantities){
        Products = products;
        Quantities = quantities;
        
        foreach(int quantity in quantities){
            TotalQuantity += quantity;
        }

        for(int i = 0; i < Products.Length; i++){
            TotalPrice += products[i].Price * quantities[i];
        }
        CreatedAt = DateTime.Now;
    }

    public String DisplayOrder(){
        string result = "";
       for(int i =0; i < Products.Length; i++){
        result += $"The {i} product of order is {Products[i].DisplayInfo()}, the quantity of this product is {Quantities[i]} ";
       }
       result += $"The total quantities of the order is {TotalQuantity}.";

       return result;
    }

    public String GetTotalPrice(){
        return $"The total price of the product is {TotalPrice}$";
    }
}
