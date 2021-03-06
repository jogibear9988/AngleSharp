﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// </summary>
    public sealed class CSSFontVariantProperty : CSSProperty
    {
        #region Fields

        FontVariant _style;

        #endregion

        #region ctor

        internal CSSFontVariantProperty()
            : base(PropertyNames.FontVariant)
        {
            _inherited = true;
            _style = FontVariant.Normal;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected font variant transformation, if any.
        /// </summary>
        public FontVariant Variant
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("normal"))
                _style = FontVariant.Normal;
            else if (value.Is("small-caps"))
                _style = FontVariant.SmallCaps;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
