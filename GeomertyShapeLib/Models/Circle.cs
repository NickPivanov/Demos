namespace GeomertyShapeLib.Models;

public sealed class Circle : ShapeBase
{
    public int Radius { get; set; }

    public Circle(int radius)
    {
        Radius = radius;
    }

    public override double GetShapeArea()
    {
        return Math.Round(Math.PI * Math.Pow(Radius, 2), 2, MidpointRounding.AwayFromZero);
    }
}
