using MediatR;

namespace AirSense.Application.CQRS.Queries.AirPolution.GetAirPollutionHistory
{
    public record GetAirPollutionHistoryQuery(Guid UserId, long Start, long End) : IRequest<string>;
}
