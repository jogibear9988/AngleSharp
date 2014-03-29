﻿namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    sealed class CSSNumber : CSSPrimitiveValue
    {
        #region Fields

        Single _value;

        #endregion

        #region ctor

        public CSSNumber(Single value)
            : base(CssUnit.Number)
        {
            _text = value.ToString(CultureInfo.InvariantCulture);
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS number.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion
    }
}