﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-spacing
    /// </summary>
    public sealed class CSSBorderSpacingProperty : CSSProperty
    {
        #region Fields

        Length _h;
        Length _v;

        #endregion

        #region ctor

        internal CSSBorderSpacingProperty()
            : base(PropertyNames.BorderSpacing)
        {
            _inherited = true;
            _h = Length.Zero;
            _v = Length.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal spacing between cells, that is the space
        /// between cells in adjacent columns.
        /// </summary>
        public Length Horizontal
        {
            get { return _h; }
        }

        /// <summary>
        /// Gets the vertical spacing between cells, that is the space
        /// between cells in adjacent rows.
        /// </summary>
        public Length Vertical
        {
            get { return _v; }
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
            var length = value.ToLength();

            if (length.HasValue)
                _h = _v = length.Value;
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;

                if (values.Length != 2)
                    return false;

                var h = values[0].ToLength();
                var v = values[1].ToLength();

                if (!h.HasValue || !v.HasValue)
                    return false;

                _h = h.Value;
                _v = v.Value;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
