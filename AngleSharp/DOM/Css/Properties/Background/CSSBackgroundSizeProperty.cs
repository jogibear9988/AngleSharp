﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-size
    /// </summary>
    public sealed class CSSBackgroundSizeProperty : CSSProperty
    {
        #region Fields

        static readonly CalcSizeMode _default = new CalcSizeMode();
        static readonly CoverSizeMode _cover = new CoverSizeMode();
        static readonly ContainSizeMode _contain = new ContainSizeMode();
        List<SizeMode> _sizes;

        #endregion

        #region ctor

        internal CSSBackgroundSizeProperty()
            : base(PropertyNames.BackgroundSize)
        {
            _inherited = false;
            _sizes = new List<SizeMode>();
            _sizes.Add(_default);
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
            if (value is CSSValueList)
                return CheckList((CSSValueList)value);
            else if (!CheckSingle(value) && value != CSSValue.Inherit)
                return false;

            return true;
        }

        static SizeMode Check(CSSValue value)
        {
            var calc = value.AsCalc();

            if (calc != null)
                return new CalcSizeMode(calc);
            else if (value.Is("auto"))
                return _default;
            else if (value.Is("cover"))
                return _cover;
            else if (value.Is("contain"))
                return _contain;

            return null;
        }

        static SizeMode Check(CSSValue horizontal, CSSValue vertical)
        {
            var width = horizontal.AsCalc();
            var height = vertical.AsCalc();

            if (width == null && !horizontal.Is("auto"))
                return null;
            else if (height == null && !vertical.Is("auto"))
                return null;

            return new CalcSizeMode(width, height);
        }

        Boolean CheckSingle(CSSValue value)
        {
            var size = Check(value);

            if (size == null)
                return false;

            _sizes.Clear();
            _sizes.Add(size);
            return true;
        }

        Boolean CheckList(CSSValueList values)
        {
            var sizes = new List<SizeMode>();
            var list = values.ToList();

            foreach (var entry in list)
            {
                while (entry.Length == 0 || entry.Length > 2)
                    return false;

                var size = entry.Length == 1 ? Check(entry[0]) : Check(entry[0], entry[1]);

                if (size == null)
                    return false;

                sizes.Add(size);
            }

            _sizes = sizes;
            return true;
        }

        #endregion

        #region Modes

        abstract class SizeMode
        {
            //TODO Add Members that make sense
        }

        /// <summary>
        /// A value that scales the background image to the specified value in
        /// the corresponding dimension. Negative values are not allowed.
        /// </summary>
        sealed class CalcSizeMode : SizeMode
        {
            CSSCalcValue _width;
            CSSCalcValue _height;

            /// <summary>
            /// The auto keyword that scales the background image in the corresponding
            /// direction such that its intrinsic proportion is maintained.
            /// </summary>
            public CalcSizeMode(CSSCalcValue width = null, CSSCalcValue height = null)
            {
                _width = width;
                _height = height;
            }
        }

        /// <summary>
        /// This keyword specifies that the background image should be scaled to
        /// be as small as possible while ensuring both its dimensions are greater
        /// than or equal to the corresponding dimensions of the background
        /// positioning area.
        /// </summary>
        sealed class CoverSizeMode : SizeMode
        {

        }

        /// <summary>
        /// This keyword specifies that the background image should be scaled to
        /// be as large as possible while ensuring both its dimensions are less
        /// than or equal to the corresponding dimensions of the background
        /// positioning area.
        /// </summary>
        sealed class ContainSizeMode : SizeMode
        {

        }

        #endregion
    }
}
