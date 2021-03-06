﻿namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DomHistorical]
    sealed class HTMLAppletElement : HTMLElement
    {
        internal HTMLAppletElement()
            : base(Tags.Applet, NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
