﻿using AirSense.Application.CQRS.Dtos.Queries;
using AirSense.Application.Interfaces.Repositories;
using MediatR;

namespace AirSense.Application.CQRS.Queries.Admins;
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersQueryDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetAllUsersQueryDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync(cancellationToken);

        return users.Select(user => new GetAllUsersQueryDto(user.Id, user.UserName, user.Photo)).ToList();
    }
}
