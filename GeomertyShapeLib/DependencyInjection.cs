using GeomertyShapeLib.Abstractions;
using GeomertyShapeLib.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace GeomertyShapeLib;

public static class DependencyInjection
{
    public static void AddShapeAreaService(this IServiceCollection services)
    {
        services.AddTransient<IShapeAreaService, ShapeAreaService>();
    }
}
