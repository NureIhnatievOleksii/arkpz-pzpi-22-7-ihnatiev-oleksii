using MediatR;

namespace AirSense.Application.CQRS.Queries.AirPolution.GetAirPollution
{
    public record GetAirPollutionQuery(Guid UserId) : IRequest<string>;
}
