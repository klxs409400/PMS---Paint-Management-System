namespace PaintStore.Model;

public class PaintSpecification
{
    public int Id { get; set; }

    public string Color { get; set; } = null!;

    public int SizeInLiters { get; set; }

    public PaintType PaintType { get; set; }

    public PaintSpecification()
    {
    }

    public PaintSpecification(string color, int sizeInLiters, PaintType paintType)
    {
        Color = color;
        SizeInLiters = sizeInLiters;
        PaintType = paintType;
    }

    public string DisplaySpecification()
    {
        return $"The Color of Paint is {Color}, The Size of Paint is {SizeInLiters} Liters, The type of Paint is {PaintType}";
    }
}
