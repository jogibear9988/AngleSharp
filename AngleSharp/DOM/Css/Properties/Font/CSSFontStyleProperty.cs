﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
    /// </summary>
    public sealed class CSSFontStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, FontStyle> _styles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase);
        FontStyle _style;

        #endregion

        #region ctor

        static CSSFontStyleProperty()
        {
            _styles.Add("normal", FontStyle.Normal);
            _styles.Add("italic", FontStyle.Italic);
            _styles.Add("oblique", FontStyle.Oblique);
        }

        internal CSSFontStyleProperty()
            : base(PropertyNames.FontStyle)
        {
            _inherited = true;
            _style = FontStyle.Normal;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected font style.
        /// </summary>
        public FontStyle Style
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
            FontStyle style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _style = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
