namespace GeomertyShapeLib.Models;

public sealed class Circle : ShapeBase
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double GetShapeArea()
    {
        return Math.Round(Math.PI * Math.Pow(Radius, 2), 2, MidpointRounding.AwayFromZero);
    }
}
