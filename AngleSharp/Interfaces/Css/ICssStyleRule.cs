﻿namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a general CSS styling rule.
    /// </summary>
    [DomName("CSSStyleRule")]
    public interface ICssStyleRule : ICssRule
    {
        /// <summary>
        /// Gets or sets the textual representation of the selector for this rule, e.g. "h1,h2".
        /// </summary>
        [DomName("selectorText")]
        String SelectorText { get; set; }

        /// <summary>
        /// Gets the CSSStyleDeclaration object for the rule.
        /// </summary>
        [DomName("style")]
        ICssStyleDeclaration Style { get; }
    }
}
