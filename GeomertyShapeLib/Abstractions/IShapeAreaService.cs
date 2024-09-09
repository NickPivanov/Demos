using GeomertyShapeLib.Models;

namespace GeomertyShapeLib.Abstractions;

public interface IShapeAreaService
{
    /// <summary>
    /// Get shape area
    /// </summary>
    /// <returns></returns>
    public double GetShapeArea<TShape>(TShape shape) where TShape : ShapeBase;
    /// <summary>
    /// Get circle area by radius
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public double GetCircleArea(double radius);
    /// <summary>
    /// Get triangle area by the sides of the triangle 
    /// </summary>
    /// <param name="sideA"></param>
    /// <param name="sideB"></param>
    /// <param name="sideC"></param>
    /// <returns></returns>
    public double GetTriangleArea(double sideA, double sideB, double sideC);
}
