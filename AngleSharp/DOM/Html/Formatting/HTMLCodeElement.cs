﻿namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The code HTML element.
    /// </summary>
    sealed class HTMLCodeElement : HTMLElement
    {
        internal HTMLCodeElement()
            : base(Tags.Code, NodeFlags.HtmlFormatting)
        {
        }
    }
}
