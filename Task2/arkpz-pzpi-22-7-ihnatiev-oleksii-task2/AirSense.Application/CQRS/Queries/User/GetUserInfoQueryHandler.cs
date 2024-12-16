using AutoMapper;
using MediatR;
using AirSense.Application.Interfaces.Repositories;
using AirSense.Application.CQRS.Dtos.Queries;

namespace AirSense.Application.CQRS.Queries.User
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoQueryDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserInfoQueryDto> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(query.UserId, cancellationToken);
            if (user == null)
                throw new Exception("User not found");

            // Возвращаем только информацию о пользователе без постов и комментариев
            return new GetUserInfoQueryDto(user.UserName, user.Email, user.Photo);
        }
    }
}
