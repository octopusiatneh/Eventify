using Eventify.Modules.Attendance.Application.Abstraction;
using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Attendance.Application.Attendees.Create;

internal sealed class CreateAttendeeCommandHandler(
    IAttendeeRepository attendeeRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateAttendeeCommand>
{
    public async Task<Result> Handle(CreateAttendeeCommand request, CancellationToken cancellationToken)
    {
        var (userId, email, firstName, lastName) = request;

        var attendee = Attendee.Create(userId, email, firstName, lastName);
        await attendeeRepository.InsertAsync(attendee, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
