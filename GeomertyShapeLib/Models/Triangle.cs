namespace GeomertyShapeLib.Models;

public sealed class Triangle : ShapeBase
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    public override double GetShapeArea()
    {
        return Math.Round(SideA * SideB * SideC, 2, MidpointRounding.AwayFromZero);
    }
}
