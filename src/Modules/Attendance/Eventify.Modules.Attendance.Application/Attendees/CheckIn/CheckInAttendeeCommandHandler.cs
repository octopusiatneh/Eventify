using Eventify.Modules.Attendance.Application.Abstraction;
using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Modules.Attendance.Domain.Tickets;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;
using Microsoft.Extensions.Logging;

namespace Eventify.Modules.Attendance.Application.Attendees.CheckIn;

internal sealed class CheckInAttendeeCommandHandler(
    IAttendeeRepository attendeeRepository,
    ITicketRepository ticketRepository,
    IUnitOfWork unitOfWork,
    ILogger<CheckInAttendeeCommandHandler> logger) : ICommandHandler<CheckInAttendeeCommand>
{
    public async Task<Result> Handle(CheckInAttendeeCommand request, CancellationToken cancellationToken)
    {
        var attendee = await attendeeRepository.GetAsync(request.AttendeeId, cancellationToken);
        if (attendee is null)
        {
            return Result.Failure(AttendeeErrors.NotFound(request.AttendeeId));
        }

        var ticket = await ticketRepository.GetAsync(request.TicketId, cancellationToken);
        if (ticket is null)
        {
            return Result.Failure(TicketErrors.NotFound(request.TicketId));
        }

        var checkInResult = attendee.CheckIn(ticket);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (checkInResult.IsFailure)
        {
            logger.LogWarning(
                "Failed to check in attendee {AttendeeId} for ticket {TicketId}: {Error}",
                request.AttendeeId,
                request.TicketId,
                checkInResult.Error.Message);
        }

        return checkInResult;
    }

}
