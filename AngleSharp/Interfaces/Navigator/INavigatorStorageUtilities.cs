﻿namespace AngleSharp.DOM.Navigator
{
    /// <summary>
    /// Defines a set of methods for working with IO.
    /// </summary>
    [DomName("NavigatorStorageUtils")]
    public interface INavigatorStorageUtilities
    {
        /// <summary>
        /// Blocks the current operation until storage operations have completed.
        /// </summary>
        [DomName("yieldForStorageUpdates")]
        void WaitForStorageUpdates();
    }
}
