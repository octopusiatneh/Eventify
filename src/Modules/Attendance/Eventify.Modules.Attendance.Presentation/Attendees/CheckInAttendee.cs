using Eventify.Modules.Attendance.Application.Abstraction;
using Eventify.Modules.Attendance.Application.Attendees.CheckIn;
using Eventify.Shared.Presentation.ApiResult;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Attendance.Presentation.Attendees;

internal sealed class CheckInAttendee : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapPut(
            "attendees/check-in",
            async (IAttendeeContext attendeeContext, Request request, ISender sender) =>
        {
            var result = await sender.Send(new CheckInAttendeeCommand(attendeeContext.AttendeeId, request.TicketId));

            return result.ToApiResponse(ApiResult.NoContent, ApiResult.Problem);
        })
            .WithTags(Tags.Attendees);
    }

    internal sealed record Request(Guid TicketId);
}
