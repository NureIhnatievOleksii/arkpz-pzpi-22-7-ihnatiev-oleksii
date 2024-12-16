using MediatR;

namespace AirSense.Application.CQRS.Queries.Location.GetLocationByCity
{
    public record GetLocationByCityQuery(string City, string State, string Country, int Limit) : IRequest<string>;
}
