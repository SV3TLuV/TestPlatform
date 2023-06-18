using System.Reflection;
using AutoMapper;
using TestPlatform.Core.Common.Interfaces;

namespace TestPlatform.Application.Common.Mapping;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly)
    {
        ApplyMappingsFromAssembly(assembly);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType
                          && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Map") ?? type.GetInterface("IMapWith`1")!.GetMethod("Map");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}