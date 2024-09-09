using GeomertyShapeLib.Abstractions;
using GeomertyShapeLib.Models;

namespace GeomertyShapeLib.Implementations;

public sealed class ShapeAreaService : IShapeAreaService
{
    public double GetShapeArea<TShape>(TShape shape) where TShape : ShapeBase
    {
        return shape.GetShapeArea();
    }

    public double GetCircleArea(double radius)
    {
        return new Circle(radius).GetShapeArea();
    }

    public double GetTriangleArea(double legA, double legB, double hypotenuse)
    {
        return new Triangle(legA, legB, hypotenuse).GetShapeArea();
    }
}
