using MediatR;
using System.Threading;
using System.Threading.Tasks;
using yenoMoG.Armura.Api.Commands;

namespace yenoMoG.Armura.Api.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        public Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
