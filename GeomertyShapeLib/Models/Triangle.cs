namespace GeomertyShapeLib.Models;

public sealed class Triangle : ShapeBase
{
    public Triangle(double sideA, double sideB, double sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    public override double GetShapeArea()
    {
        return Math.Round(SideA * SideB * SideC, 2, MidpointRounding.AwayFromZero);
    }
}
