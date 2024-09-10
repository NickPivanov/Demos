namespace GeomertyShapeLib.Models;

public class Triangle : ShapeBase
{
    public Triangle(double legA, double legB, double hypotenuse)
    {
        LegA = legA;
        LegB = legB;
        Hypotenuse = hypotenuse;
    }

    protected Triangle()
    { }

    public double LegA { get; set; }
    public double LegB { get; set; }
    public double Hypotenuse { get; set; }

    public override double GetShapeArea()
    {
        return Math.Round(LegA * LegB * Hypotenuse, 2, MidpointRounding.AwayFromZero);
    }
}
