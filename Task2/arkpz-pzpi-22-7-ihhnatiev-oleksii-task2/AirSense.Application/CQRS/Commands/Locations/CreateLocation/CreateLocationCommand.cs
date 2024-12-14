using MediatR;

namespace AirSense.Application.CQRS.Commands.Locations.CreateLocation;

public record CreateLocationCommand(
    double Latitude,
    double Longitude,
    Guid UserId
) : IRequest;
