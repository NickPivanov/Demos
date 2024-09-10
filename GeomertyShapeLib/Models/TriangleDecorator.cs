namespace GeomertyShapeLib.Models;

public class TriangleDecorator : Triangle
{
    public TriangleDecorator(Triangle triangle)
    {
        LegA = triangle.LegA;
        LegB = triangle.LegB;
        Hypotenuse = triangle.Hypotenuse;
    }

    /// <summary>
    /// Is triangle right angled
    /// </summary>
    /// <returns></returns>
    public bool IsRightAngled()
    {
        return Math.Pow(Hypotenuse, 2) == Math.Pow(LegA, 2) + Math.Pow(LegB, 2);
    }
}
