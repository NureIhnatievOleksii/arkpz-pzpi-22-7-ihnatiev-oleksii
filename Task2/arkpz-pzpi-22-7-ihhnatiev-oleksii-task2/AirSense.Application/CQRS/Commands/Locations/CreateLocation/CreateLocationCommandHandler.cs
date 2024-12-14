using AutoMapper;
using MediatR;
using AirSense.Application.Interfaces.Repositories;
using AirSense.Domain.LocationAggregate;

namespace AirSense.Application.CQRS.Commands.Locations.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand>
{
    private readonly ILocationRepository _repository;
    private readonly IMapper _mapper;

    public CreateLocationCommandHandler(ILocationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        var location = _mapper.Map<Location>(command);

        await _repository.CreateAsync(location, cancellationToken);
    }
}
