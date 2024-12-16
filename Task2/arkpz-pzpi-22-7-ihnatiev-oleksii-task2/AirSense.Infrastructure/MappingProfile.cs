using AirSense.Application.CQRS.Commands.Locations.CreateLocation;
using AirSense.Domain.LocationAggregate;
using AutoMapper;

namespace AirSense.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateLocationCommand, Location>();
    }
}
