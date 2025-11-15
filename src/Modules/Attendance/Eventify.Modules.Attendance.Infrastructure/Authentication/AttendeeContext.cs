using Eventify.Modules.Attendance.Application.Abstraction;
using Eventify.Shared.Application.Authentication;
using Eventify.Shared.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Eventify.Modules.Attendance.Infrastructure.Authentication;

internal sealed class AttendeeContext(IHttpContextAccessor httpContextAccessor) : IAttendeeContext
{
    public Guid AttendeeId => httpContextAccessor.HttpContext?.User.GetUserId()
        ?? throw new EventifyException("Attendee identifier is unavailable");
}
