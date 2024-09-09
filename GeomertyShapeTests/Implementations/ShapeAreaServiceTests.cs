using GeomertyShapeLib.Abstractions;
using GeomertyShapeLib.Implementations;
using GeomertyShapeLib.Models;

namespace GeomertyShapeTests.Implementations;

public class ShapeAreaServiceTests
{
    private readonly IShapeAreaService _shapeAreaService;
    private Circle _demoCircle;
    private Triangle _demoTriangle;
    public ShapeAreaServiceTests()
    {
        _shapeAreaService = new ShapeAreaService();
        _demoCircle = new Circle(10);
        _demoTriangle = new Triangle(10, 10,10);
    }

    [Theory]
    [InlineData(1, 3.14)]
    public void Can_GetCircleArea(double radius, double result)
    {
        var area = _shapeAreaService.GetCircleArea(radius);
        Assert.Equal(result, area);
    }

    [Theory]
    [InlineData(3, 3, 3, 27)]
    public void Can_GetTriangleArea(double legA, double legB, double hypotenuse, double result)
    {
        var area = _shapeAreaService.GetTriangleArea(legA, legB, hypotenuse);
        Assert.Equal(result, area);
    }

    [Fact]
    public void Can_GetShapeArea()
    {
        var area = _shapeAreaService.GetShapeArea(_demoCircle);
        Assert.Equal(314.16, area);
    }
}
