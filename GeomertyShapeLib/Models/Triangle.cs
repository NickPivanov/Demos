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
        var p = (LegA + LegB + Hypotenuse) / 2;
        var height = 2 * Math.Sqrt(p * (p - LegA)*(p - LegB)*(p - Hypotenuse))/LegA;
        return Math.Round(LegA * height / 2, 2, MidpointRounding.AwayFromZero);
    }
}
