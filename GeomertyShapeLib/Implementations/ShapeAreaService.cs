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
        if (radius <= 0)
            throw new ($"{nameof(radius)} should be greater then 0" );
        
        return new Circle(radius).GetShapeArea();
    }

    public double GetTriangleArea(double legA, double legB, double hypotenuse)
    {
        if (legA <= 0 || legB <= 0 || hypotenuse <= 0)
            throw new("Triangle should be greater then 0");

        return new Triangle(legA, legB, hypotenuse).GetShapeArea();
    }
}
