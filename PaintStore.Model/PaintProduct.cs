namespace PaintStore.Model;

public class PaintProduct : IBuyable
{
    public const decimal DefaultDiscount = 0.05m;

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public PaintType Type { get; set; }

    public PaintSpecification Specification { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal TaxRate { get; set; } = 0.1m;

    public Brand Brand { get; set; } = null!;

    public DateTime CreatedDate { get; set; }


    public PaintProduct()
    {

    }

    public decimal GetFinalPrice()
    {
        decimal finalPrice = Price * (1 - DefaultDiscount);
        finalPrice *= 1 + TaxRate;
        return finalPrice;
    }

    public decimal GetMaxDiscount(decimal rate, bool isOverridable)
    {
        if (isOverridable && rate > DefaultDiscount)
        {
            return rate;
        }
        return DefaultDiscount;
    }

    public string DisplayInfo()
    {
        return $"The type of paint is {Type}, the Price of paint is {Price}, the Name of PaintProduct is {Name}, the specification of paint is {Specification.DisplaySpecification()}, The brand of the paint is {Brand.BrandName}";
    }
}
