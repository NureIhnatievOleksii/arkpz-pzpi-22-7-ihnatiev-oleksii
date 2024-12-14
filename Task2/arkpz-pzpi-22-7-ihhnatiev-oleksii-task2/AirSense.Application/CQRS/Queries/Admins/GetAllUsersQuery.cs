using AirSense.Application.CQRS.Dtos.Queries;
using MediatR;

namespace AirSense.Application.CQRS.Queries.Admins;
public record GetAllUsersQuery : IRequest<List<GetAllUsersQueryDto>>;

