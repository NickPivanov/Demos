namespace GeomertyShapeLib.Models;

public class TriangleDecorator
{
    private readonly Triangle _triangle;
    public TriangleDecorator(Triangle triangle)
    {
        _triangle = triangle;
    }

    /// <summary>
    /// Is triangle right angled
    /// </summary>
    /// <returns></returns>
    public bool IsRightAngled()
    {
        return Math.Pow(_triangle.Hypotenuse, 2) == Math.Pow(_triangle.LegA, 2) + Math.Pow(_triangle.LegB, 2);
    }
}
