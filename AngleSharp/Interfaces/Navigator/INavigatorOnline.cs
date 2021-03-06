﻿namespace AngleSharp.DOM.Navigator
{
    using System;

    /// <summary>
    /// Connectivity information regarding the navigator.
    /// </summary>
    [DomName("NavigatorOnLine")]
    public interface INavigatorOnline
    {
        /// <summary>
        /// Gets if the connection is established.
        /// </summary>
        [DomName("onLine")]
        Boolean IsOnline { get; }
    }
}
