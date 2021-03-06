﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-width
    /// </summary>
    public sealed class CSSOutlineWidthProperty : CSSProperty
    {
        #region Fields

        Length _width;

        #endregion

        #region ctor

        internal CSSOutlineWidthProperty()
            : base(PropertyNames.OutlineWidth)
        {
            _inherited = false;
            _width = Length.Medium;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the outline of an element. An outline is a
        /// line that is drawn around elements, outside the border edge,
        /// to make the element stand out:
        /// </summary>
        public Length Width
        {
            get { return _width; }
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
            var length = value.ToBorderWidth();

            if (length.HasValue)
                _width = length.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
