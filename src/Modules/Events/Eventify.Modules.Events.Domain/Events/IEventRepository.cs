﻿namespace Eventify.Modules.Events.Domain.Events;

public interface IEventRepository
{
    void Insert(Event @event);
}
