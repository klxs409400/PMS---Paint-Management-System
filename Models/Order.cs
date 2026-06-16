using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Xml.XPath;
using PaintManagementSystem.Enums;

namespace PaintManagementSystem.Models;

public class Order
{
    public readonly DateTime CreatedAt;

    public int OrderId { get; set; }

    public List<PaintProduct> Products { get; set; }

    public int[] Quantities { get; set; }

    public int TotalQuantity {get; set;}

    public decimal TotalPrice { get; private set; }

    public Order(List<PaintProduct> products, int[] quantities, int Orderid){
        Products = products;
        Quantities = quantities;
        
        foreach(int quantity in quantities){
            TotalQuantity += quantity;
        }

        for(int i = 0; i < Products.Count; i++){
            TotalPrice += products[i].Price * quantities[i];
        }
        CreatedAt = DateTime.Now;

        OrderId = Orderid;
    }

    public void GetMostExpensivePaintProduct(){
        PaintProduct? maxPrice = Products.OrderByDescending(p => p.Price).FirstOrDefault();
        System.Console.WriteLine(maxPrice?.DisplayInfo());
    }

    public void RemoveProduct(int productId){
        Products.RemoveAll(p => p.ProductId == productId);
    }

    public List<PaintProduct> SpecificPaint(decimal maxPrice, decimal minPrice){
        List<PaintProduct> relatedPaint = Products.FindAll(p => p.Price > minPrice && p.Price<maxPrice);
        return relatedPaint;
    }

    public decimal FindTotalPrice(PaintType type){
        decimal TotalPrice = 0;
        TotalPrice = Products.Where(p => p.Type == type).Select(p => p.Price).Sum();
        return TotalPrice;
    }

    public bool CheckIfHasNullName(){
        return Products.Any(p => p.Name == null);
    }


    public String DisplayOrder(){
        string result = "";
       for(int i =0; i < Products.Count; i++){
        result += $"The {i} product of order{OrderId} is {Products[i].DisplayInfo()}, the quantity of this product is {Quantities[i]} ";
       }
       result += $"The total quantities of the order{OrderId} is {TotalQuantity}, and it is created at {CreatedAt}";

       return result;
    }

    public String GetTotalPrice(){
        return $"The total price of the product is {TotalPrice}$";
    }
}
