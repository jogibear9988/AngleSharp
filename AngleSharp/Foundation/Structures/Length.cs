﻿namespace AngleSharp
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a length value.
    /// </summary>
    public struct Length : IEquatable<Length>, ICssObject
    {
        #region Fields

        /// <summary>
        /// Gets a zero pixel length value.
        /// </summary>
        public static readonly Length Zero = new Length();

        /// <summary>
        /// Gets a thin length value.
        /// </summary>
        public static readonly Length Thin = new Length(1f, Unit.Px);

        /// <summary>
        /// Gets a medium length value.
        /// </summary>
        public static readonly Length Medium = new Length(3f, Unit.Px);

        /// <summary>
        /// Gets a thick length value.
        /// </summary>
        public static readonly Length Thick = new Length(5f, Unit.Px);

        Single _value;
        Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new length value.
        /// </summary>
        /// <param name="value">The value of the length.</param>
        /// <param name="unit">The unit of the length.</param>
        public Length(Single value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Checks the equality of the two given lengths.
        /// </summary>
        /// <param name="a">The left length.</param>
        /// <param name="b">The right length.</param>
        /// <returns>True if both lengths are equal, otherwise false.</returns>
        public static Boolean operator ==(Length a, Length b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks the inequality of the two given lengths.
        /// </summary>
        /// <param name="a">The left length.</param>
        /// <param name="b">The right length.</param>
        /// <returns>True if both lengths are not equal, otherwise false.</returns>
        public static Boolean operator !=(Length a, Length b)
        {
            return !a.Equals(b);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the length to a number of pixels, if possible.
        /// </summary>
        /// <returns>The number of pixels represented by the current length.</returns>
        public Single ToPixel()
        {
            switch (_unit)
            {
                case Unit.In: // 1 in = 2.54 cm
                    return _value * 96f;
                case Unit.Mm: // 1 mm = 0.1 cm
                    return _value * 5f * 96f / 127f;
                case Unit.Pc: // 1 pc = 12 pt
                    return _value * 12f * 96f / 72f;
                case Unit.Pt: // 1 pt = 1/72 in
                    return _value * 96f / 72f;
                case Unit.Cm: // 1 cm = 50/127 in
                    return _value * 50f * 96f / 127f;
                case Unit.Px: // 1 px = 1/96 in
                default:
                    return _value;
            }
        }

        /// <summary>
        /// Checks if both lengths are actually equal.
        /// </summary>
        /// <param name="other">The other length to compare to.</param>
        /// <returns>True if both lengths are equal, otherwise false.</returns>
        public Boolean Equals(Length other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of length units.
        /// </summary>
        public enum Unit : ushort
        {
            /// <summary>
            /// The value is a length (px).
            /// </summary>
            Px,
            /// <summary>
            /// The value is a length (em).
            /// </summary>
            Em,
            /// <summary>
            /// The value is a length (ex). Usually about 0.5em for most fonts.
            /// </summary>
            Ex,
            /// <summary>
            /// The value is a length (cm).
            /// </summary>
            Cm,
            /// <summary>
            /// The value is a length (mm).
            /// </summary>
            Mm,
            /// <summary>
            /// The value is a length (in).
            /// </summary>
            In,
            /// <summary>
            /// The value is a length (pt).
            /// </summary>
            Pt,
            /// <summary>
            /// The value is a length (pc).
            /// </summary>
            Pc,
            /// <summary>
            /// The value is a length (width of the 0-character).
            /// </summary>
            Ch,
            /// <summary>
            /// The value is the relative to the font-size of the root element (in em).
            /// </summary>
            Rem,
            /// <summary>
            /// The value is relative to the viewport width.
            /// </summary>
            Vw,
            /// <summary>
            /// The value is relative to the viewport height.
            /// </summary>
            Vh,
            /// <summary>
            /// The value is relative to the minimum of viewport width and height.
            /// </summary>
            Vmin,
            /// <summary>
            /// The value is relative to the maximum of viewport width and height.
            /// </summary>
            Vmax,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is Length)
                return this.Equals((Length)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current length.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return (Int32)_value;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a CSS representation of the length.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        public String ToCss()
        {
            if (_value == 0f)
                return _value.ToString(CultureInfo.InvariantCulture);

            return String.Concat(_value.ToString(CultureInfo.InvariantCulture), _unit.ToString().ToLower());
        }

        /// <summary>
        /// Returns a string representing the length.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(), _unit.ToString().ToLower());
        }

        #endregion
    }
}
