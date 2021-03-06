﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// </summary>
    public sealed class CSSFontSizeProperty : CSSProperty
    {
        #region Fields

        FontSize _mode;
        CSSCalcValue _size;

        #endregion

        #region ctor

        internal CSSFontSizeProperty()
            : base(PropertyNames.FontSize)
        {
            _mode = FontSize.Medium;
            _size = null;
            _inherited = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the custom set font-size, if any.
        /// </summary>
        public CSSCalcValue Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Gets the font-size mode.
        /// </summary>
        public FontSize Mode
        {
            get { return _mode; }
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
            //TODO
            //UNITLESS in QUIRKSMODE
            FontSize? size;
            var calc = value.AsCalc();

            if (calc != null)
            {
                _size = calc;
                _mode = FontSize.Custom;
            }
            else if ((size = value.ToFontSize()).HasValue)
            {
                _size = null;
                _mode = size.Value;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
