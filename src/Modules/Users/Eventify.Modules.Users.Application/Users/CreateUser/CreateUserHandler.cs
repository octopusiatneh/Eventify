using Eventify.Modules.Users.Application.Abstractions.Data;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Users.Application.Users.CreateUser;

public sealed class CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var (email, firstName, lastName) = request;
        var user = User.Create(email, firstName, lastName);
        
        await userRepository.InsertAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        user.Raise(new UserCreated(user.Id));

        return user.Id;
    }
}
