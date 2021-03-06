﻿namespace AngleSharp.DOM.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS value.
	/// </summary>
    public class CSSValue : ICssValue, ICssObject
    {
        #region Fields

        /// <summary>
        /// The type of value.
        /// </summary>
        protected CssValueType _type;

        /// <summary>
        /// Gets the instance for a slash delimiter value.
        /// </summary>
        internal static readonly CSSValue Delimiter = new CSSValue { _type = CssValueType.Custom };

        /// <summary>
        /// Gets the instance for a comma separator value.
        /// </summary>
        internal static readonly CSSValue Separator = new CSSValue { _type = CssValueType.Custom };

        /// <summary>
        /// Gets the instance for an inherited value.
        /// </summary>
        public static readonly CSSValue Inherit = new CSSValue { _type = CssValueType.Inherit };

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        internal CSSValue()
        {
            _type = CssValueType.Custom;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a code defining the type of the value as defined above.
		/// </summary>
        public CssValueType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets a string representation of the current value.
        /// </summary>
        public String CssText
        {
            get { return ToCss(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public virtual String ToCss()
        {
            return this == Inherit ? CssParser.Inherit : (this == Separator ? "," : (this == Delimiter ? "/" : String.Empty));
        }

        #endregion
    }
}
