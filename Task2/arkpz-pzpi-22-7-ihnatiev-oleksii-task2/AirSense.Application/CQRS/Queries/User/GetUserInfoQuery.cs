using AirSense.Application.CQRS.Dtos.Queries;
using MediatR;

namespace AirSense.Application.CQRS.Queries.User
{
   public record GetUserInfoQuery(Guid UserId) : IRequest<GetUserInfoQueryDto>;

}
