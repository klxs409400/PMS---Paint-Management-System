using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using PaintManagementSystem.Enums;
using PaintManagementSystem.Interfaces;

namespace PaintManagementSystem.Models;

public class PaintProduct : IBuyable
{
    public readonly decimal TaxRate;
    public const decimal DefaultDiscount = 0.05m;
    public string Name { get; set; }

    public PaintType Type { get; set; }

    public PaintSpecification Specification { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public Brand BrandName { get; set; }

    public PaintProduct(string name, PaintType type, PaintSpecification specification, decimal price, Brand brandname, int productid){
        Name = name;
        Type = type;
        Specification = specification;
        Price = price;
        TaxRate = 0.1m;
        BrandName = brandname;
        ProductId = productid;

    }

    public decimal GetFinalPrice(){
        decimal finalprice1 = Price * (1-DefaultDiscount);
        
        decimal finalprice2 = finalprice1 * (1+ TaxRate);

        return finalprice2;
    }

    public String DisplayInfo(){
       return  $"The type of paint is {Type}, the Price of paint is {Price}, the Name of PaintProduct is {Name}, the specification of paint is {Specification.DisplaySpecification()}, The brand of the paint is {BrandName.BrandName}";
    }

    public decimal GetMaxDiscount(decimal rate, bool isOverridable){
        if (isOverridable){
            if(rate > DefaultDiscount){
                return rate;
            }
            return DefaultDiscount;
        }
        return DefaultDiscount;
    }
}
