using AutoMapper;

namespace TestPlatform.Core.Common.Interfaces;

public interface IMapWith<T>
{
    public void Map(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType())
            .ReverseMap();
    }
}