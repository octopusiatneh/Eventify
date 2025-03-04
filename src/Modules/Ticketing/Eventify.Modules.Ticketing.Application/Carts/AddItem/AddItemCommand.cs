﻿using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Ticketing.Application.Carts.AddItem;

public sealed record AddItemCommand(Guid CustomerId, Guid TicketTypeId, int Quantity) : ICommand;
