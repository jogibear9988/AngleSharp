﻿namespace AngleSharp.DOM
{
    /// <summary>
    /// Defines the callback signature for an event.
    /// </summary>
    /// <param name="ev">The event arguments.</param>
    [DomName("EventHandler")]
    public delegate void EventListener(IEvent ev);
}
