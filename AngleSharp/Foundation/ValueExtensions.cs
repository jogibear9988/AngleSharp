﻿namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A bunch of useful extension methods.
    /// </summary>
    static class ValueExtensions
    {
        #region Dictionaries

        static readonly Dictionary<String, LineStyle> lineStyles = new Dictionary<String, LineStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, BoxModel> boxModels = new Dictionary<String, BoxModel>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, CSSTimingValue> timingFunctions = new Dictionary<String, CSSTimingValue>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, AnimationFillStyle> fillModes = new Dictionary<String, AnimationFillStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, AnimationDirection> directions = new Dictionary<String, AnimationDirection>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Visibility> visibilities = new Dictionary<String, Visibility>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, ListStyle> listStyles = new Dictionary<String, ListStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, ListPosition> listPositions = new Dictionary<String, ListPosition>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, FontSize> fontSizes = new Dictionary<String, FontSize>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Initial Population

        static ValueExtensions()
        {
            lineStyles.Add("none", LineStyle.None);
            lineStyles.Add("solid", LineStyle.Solid);
            lineStyles.Add("double", LineStyle.Double);
            lineStyles.Add("dotted", LineStyle.Dotted);
            lineStyles.Add("dashed", LineStyle.Dashed);
            lineStyles.Add("inset", LineStyle.Inset);
            lineStyles.Add("outset", LineStyle.Outset);
            lineStyles.Add("ridge", LineStyle.Ridge);
            lineStyles.Add("groove", LineStyle.Groove);
            lineStyles.Add("hidden", LineStyle.Hidden);

            boxModels.Add("border-box", BoxModel.BorderBox);
            boxModels.Add("padding-box", BoxModel.PaddingBox);
            boxModels.Add("content-box", BoxModel.ContentBox);

            timingFunctions.Add("ease", CSSTimingValue.Ease);
            timingFunctions.Add("ease-in", CSSTimingValue.EaseIn);
            timingFunctions.Add("ease-out", CSSTimingValue.EaseOut);
            timingFunctions.Add("ease-in-out", CSSTimingValue.EaseInOut);
            timingFunctions.Add("linear", CSSTimingValue.Linear);
            timingFunctions.Add("step-start", CSSTimingValue.StepStart);
            timingFunctions.Add("step-end", CSSTimingValue.StepEnd);

            fillModes.Add("none", AnimationFillStyle.None);
            fillModes.Add("forwards", AnimationFillStyle.Forwards);
            fillModes.Add("backwards", AnimationFillStyle.Backwards);
            fillModes.Add("both", AnimationFillStyle.Both);

            directions.Add("normal", AnimationDirection.Normal);
            directions.Add("reverse", AnimationDirection.Reverse);
            directions.Add("alternate", AnimationDirection.Alternate);
            directions.Add("alternate-reverse", AnimationDirection.AlternateReverse);

            visibilities.Add("visible", Visibility.Visible);
            visibilities.Add("hidden", Visibility.Hidden);
            visibilities.Add("collapse", Visibility.Collapse);

            listStyles.Add("disc", ListStyle.Disc);
            listStyles.Add("circle", ListStyle.Circle);
            listStyles.Add("square", ListStyle.Square);
            listStyles.Add("decimal", ListStyle.Decimal);
            listStyles.Add("decimal-leading-zero", ListStyle.DecimalLeadingZero);
            listStyles.Add("lower-roman", ListStyle.LowerRoman);
            listStyles.Add("upper-roman", ListStyle.UpperRoman);
            listStyles.Add("lower-greek", ListStyle.LowerGreek);
            listStyles.Add("lower-latin", ListStyle.LowerLatin);
            listStyles.Add("upper-latin", ListStyle.UpperLatin);
            listStyles.Add("armenian", ListStyle.Armenian);
            listStyles.Add("georgian", ListStyle.Georgian);
            listStyles.Add("lower-alpha", ListStyle.LowerLatin);
            listStyles.Add("upper-alpha", ListStyle.UpperLatin);
            listStyles.Add("none", ListStyle.None);

            listPositions.Add("inside", ListPosition.Inside);
            listPositions.Add("outside", ListPosition.Outside);

            fontSizes.Add("xx-small", FontSize.Tiny);
            fontSizes.Add("x-small", FontSize.Little);
            fontSizes.Add("small", FontSize.Small);
            fontSizes.Add("medium", FontSize.Medium);
            fontSizes.Add("large", FontSize.Large);
            fontSizes.Add("x-large", FontSize.Big);
            fontSizes.Add("xx-large", FontSize.Huge);
            fontSizes.Add("larger", FontSize.Smaller);
            fontSizes.Add("smaller", FontSize.Larger);
        }

        #endregion

        #region Dictionary Lookups

        public static AnimationDirection? ToDirection(this CSSValue value)
        {
            AnimationDirection direction;

            if (value is CSSIdentifierValue && directions.TryGetValue(((CSSIdentifierValue)value).Value, out direction))
                return direction;

            return null;
        }

        public static AnimationFillStyle? ToFillMode(this CSSValue value)
        {
            AnimationFillStyle fillMode;

            if (value is CSSIdentifierValue && fillModes.TryGetValue(((CSSIdentifierValue)value).Value, out fillMode))
                return fillMode;

            return null;
        }

        public static LineStyle? ToLineStyle(this CSSValue value)
        {
            LineStyle style;

            if (value is CSSIdentifierValue && lineStyles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                return style;

            return null;
        }

        public static Visibility? ToVisibility(this CSSValue value)
        {
            Visibility visibility;

            if (value is CSSIdentifierValue && visibilities.TryGetValue(((CSSIdentifierValue)value).Value, out visibility))
                return visibility;

            return null;
        }

        public static CSSTimingValue ToTimingFunction(this CSSValue value)
        {
            CSSTimingValue function;

            if (value is CSSTimingValue)
                return (CSSTimingValue)value;
            else if (value is CSSIdentifierValue && timingFunctions.TryGetValue(((CSSIdentifierValue)value).Value, out function))
                return function;

            return null;
        }

        public static BoxModel? ToBoxModel(this CSSValue value)
        {
            BoxModel model;

            if (value is CSSIdentifierValue && boxModels.TryGetValue(((CSSIdentifierValue)value).Value, out model))
                return model;

            return null;
        }

        public static ListStyle? ToListStyle(this CSSValue value)
        {
            ListStyle style;

            if (value is CSSIdentifierValue && listStyles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                return style;

            return null;
        }

        public static ListPosition? ToListPosition(this CSSValue value)
        {
            ListPosition position;

            if (value is CSSIdentifierValue && listPositions.TryGetValue(((CSSIdentifierValue)value).Value, out position))
                return position;

            return null;
        }

        public static FontSize? ToFontSize(this CSSValue value)
        {
            FontSize size;

            if (value is CSSIdentifierValue && fontSizes.TryGetValue(((CSSIdentifierValue)value).Value, out size))
                return size;

            return null;
        }

        #endregion

        #region Transformers

        public static Boolean Is(this CSSValue value, String identifier)
        {
            return value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals(identifier, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean IsOneOf(this CSSValue value, params String[] identifiers)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                foreach (var identifier in identifiers)
                {
                    if (ident.Equals(identifier, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        public static Url ToUri(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Url>)
                return ((CSSPrimitiveValue<Url>)value).Value;

            return null;
        }

        public static String ToContent(this CSSValue value)
        {
            if (value is CSSStringValue)
                return ((CSSStringValue)value).Value;

            return null;
        }

        public static CSSPrimitiveValue<Color> AsColor(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Color>)
                return (CSSPrimitiveValue<Color>)value;
            else if (value is CSSIdentifierValue)
            {
                var color = Color.FromName(((CSSIdentifierValue)value).Value);

                if (color.HasValue)
                    return new CSSPrimitiveValue<Color>(color.Value);
            }

            return null;
        }

        public static CSSCalcValue AsCalc(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Percent>)
                return CSSCalcValue.FromPercent(((CSSPrimitiveValue<Percent>)value).Value);
            else if (value is CSSPrimitiveValue<Length>)
                return CSSCalcValue.FromLength(((CSSPrimitiveValue<Length>)value).Value);
            else if (value is CSSCalcValue)
                return (CSSCalcValue)value;
            else if (value is CSSPrimitiveValue<Number> && ((CSSPrimitiveValue<Number>)value).Value == Number.Zero)
                return CSSCalcValue.Zero;

            return null;
        }

        public static List<T> AsList<T>(this CSSValue value, Func<CSSValue, T> transformer = null)
            where T : CSSValue
        {
            transformer = transformer ?? (v => v as T);

            if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var list = new List<T>();

                for (int i = 0; i < values.Length; i++)
                {
                    var item = transformer(values[i++]);

                    if (item == null)
                        return null;

                    list.Add(item);

                    if (i < values.Length && values[i] != CSSValue.Separator)
                        return null;
                }

                return list;
            }
            else
            {
                var item = transformer(value);

                if (item != null)
                {
                    var list = new List<T>();
                    list.Add(item);
                    return list;
                }
            }

            return null;
        }

        public static CSSImageValue AsImage(this CSSValue value)
        {
            if (value is CSSImageValue)
                return (CSSImageValue)value;
            else if (value is CSSPrimitiveValue<Url>)
                return CSSImageValue.FromUrl(((CSSPrimitiveValue<Url>)value).Value);
            else if (value.Is("none"))
                return CSSImageValue.None;

            return null;
        }

        public static Percent? ToPercent(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Percent>)
                return ((CSSPrimitiveValue<Percent>)value).Value;

            return null;
        }

        public static Single? ToNumber(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Number>)
                return (Single)((CSSPrimitiveValue<Number>)value).Value;

            return null;
        }

        public static Int32? ToInteger(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Number>)
                return (Int32)((CSSPrimitiveValue<Number>)value).Value;

            return null;
        }

        public static Byte? ToByte(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Number>)
                return (Byte)Math.Min(Math.Max((Int32)((CSSPrimitiveValue<Number>)value).Value, 0), 255);

            return null;
        }

        public static Angle? ToAngle(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Angle>)
                return ((CSSPrimitiveValue<Angle>)value).Value;
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;

                if (values.Length == 2 && values[0].Is("to"))
                {
                    if (values[1].Is("bottom"))
                        return new Angle(180f, Angle.Unit.Deg);
                    else if (values[1].Is("right"))
                        return new Angle(90f, Angle.Unit.Deg);
                    else if (values[1].Is("left"))
                        return new Angle(270f, Angle.Unit.Deg);
                    else if (values[1].Is("top"))
                        return new Angle(0f, Angle.Unit.Deg);
                }
            }

            return null;
        }

        public static Length? ToLength(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Length>)
                return ((CSSPrimitiveValue<Length>)value).Value;
            else if (value is CSSPrimitiveValue<Number> && ((CSSPrimitiveValue<Number>)value).Value == Number.Zero)
                return Length.Zero;

            return null;
        }

        public static Resolution? ToResolution(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Resolution>)
                return ((CSSPrimitiveValue<Resolution>)value).Value;

            return null;
        }

        public static Time? ToTime(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Time>)
                return ((CSSPrimitiveValue<Time>)value).Value;

            return null;
        }

        public static Single? ToAspectRatio(this CSSValue value)
        {
            var values = value as CSSValueList;

            if (values != null && values.Length == 3 && values[1] == CSSValueList.Delimiter)
            {
                var w = values[0].ToNumber();
                var h = values[2].ToNumber();

                if (w.HasValue && h.HasValue)
                    return w.Value / h.Value;
            }

            return null;
        }

        public static Length? ToBorderWidth(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Length>)
                return ((CSSPrimitiveValue<Length>)value).Value;
            else if (value is CSSPrimitiveValue<Number> && ((CSSPrimitiveValue<Number>)value).Value == Number.Zero)
                return Length.Zero;
            else if (value.Is("thin"))
                return Length.Thin;
            else if (value.Is("medium"))
                return Length.Medium;
            else if (value.Is("thick"))
                return Length.Thick;

            return null;
        }

        public static Color? ToColor(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Color>)
                return ((CSSPrimitiveValue<Color>)value).Value;
            else if (value is CSSIdentifierValue)
                return Color.FromName(((CSSIdentifierValue)value).Value);

            return null;
        }

        public static List<CSSValueList> ToList(this CSSValueList values)
        {
            var list = new List<CSSValueList>();

            for (int i = 0; i < values.Length; i++)
            {
                var entry = new CSSValueList();

                for (int j = i; j < values.Length; j++)
                {
                    if (values[j] == CSSValue.Separator)
                        break;

                    entry.Add(values[j]);
                    i++;
                }

                list.Add(entry);
            }

            return list;
        }

        public static Point2d ToPoint(this CSSValueList values)
        {
            if (values.Length == 1)
            {
                var value = values[0];
                var calc = value.AsCalc();

                if (calc != null)
                    return new Point2d(calc);
                else if (value.Is("left"))
                    return new Point2d(x: CSSCalcValue.Zero);
                else if (value.Is("right"))
                    return new Point2d(x: CSSCalcValue.Full);
                else if (value.Is("top"))
                    return new Point2d(y: CSSCalcValue.Zero);
                else if (value.Is("bottom"))
                    return new Point2d(y: CSSCalcValue.Full);
                else if (value.Is("center"))
                    return Point2d.Centered;
            }
            else if (values.Length == 2)
            {
                var left = values[0];
                var right = values[1];
                var horizontal = left.AsCalc();
                var vertical = right.AsCalc();

                if (horizontal == null)
                {
                    if (left.Is("left"))
                        horizontal = CSSCalcValue.Zero;
                    else if (left.Is("right"))
                        horizontal = CSSCalcValue.Full;
                    else if (left.Is("center"))
                        horizontal = CSSCalcValue.Center;
                    else if (left.Is("top"))
                    {
                        horizontal = vertical;
                        vertical = CSSCalcValue.Zero;
                    }
                    else if (left.Is("bottom"))
                    {
                        horizontal = vertical;
                        vertical = CSSCalcValue.Full;
                    }
                }

                if (vertical == null)
                {
                    if (right.Is("top"))
                        vertical = CSSCalcValue.Zero;
                    else if (right.Is("bottom"))
                        vertical = CSSCalcValue.Full;
                    else if (right.Is("center"))
                        vertical = CSSCalcValue.Center;
                    else if (right.Is("left"))
                    {
                        vertical = horizontal;
                        horizontal = CSSCalcValue.Zero;
                    }
                    else if (right.Is("right"))
                    {
                        vertical = horizontal;
                        horizontal = CSSCalcValue.Full;
                    }
                }

                if (horizontal != null && vertical != null)
                    return new Point2d(horizontal, vertical);
            }
            else if (values.Length > 2)
            {
                var index = 0;
                var shift = CSSCalcValue.Zero;
                var horizontal = CSSCalcValue.Center;
                var vertical = CSSCalcValue.Center;
                var value = values[index];

                if (value.Is("left"))
                {
                    horizontal = CSSCalcValue.Zero;
                    shift = values[index + 1].AsCalc();
                }
                else if (value.Is("right"))
                {
                    horizontal = CSSCalcValue.Full;
                    shift = values[index + 1].AsCalc();
                }
                else if (!value.Is("center"))
                    return null;

                if (shift != null && shift != CSSCalcValue.Zero)
                {
                    index++;
                    horizontal = horizontal.Add(shift);
                    shift = CSSCalcValue.Zero;
                }

                value = values[++index];

                if (value.Is("top"))
                {
                    vertical = CSSCalcValue.Zero;

                    if (index + 1 < values.Length)
                        shift = values[index + 1].AsCalc();
                }
                else if (value.Is("bottom"))
                {
                    vertical = CSSCalcValue.Full;

                    if (index + 1 < values.Length)
                        shift = values[index + 1].AsCalc();
                }
                else if (!value.Is("center"))
                    return null;

                if (shift != null)
                {
                    if (shift != CSSCalcValue.Zero)
                        vertical = vertical.Add(shift);

                    return new Point2d(horizontal, vertical);
                }
            }

            return null;
        }

        public static Shadow ToShadow(this CSSValueList item)
        {
            if (item.Length < 2)
                return null;

            var inset = item[0].Is("inset");
            var offset = inset ? 1 : 0;

            if (inset && item.Length < 3)
                return null;

            var offsetX = item[offset++].ToLength();
            var offsetY = item[offset++].ToLength();

            if (!offsetX.HasValue || !offsetY.HasValue)
                return null;

            var blurRadius = Length.Zero;
            var spreadRadius = Length.Zero;
            var color = Color.Black;

            if (item.Length > offset)
            {
                var blur = item[offset].ToLength();

                if (blur.HasValue)
                {
                    blurRadius = blur.Value;
                    offset++;
                }
            }

            if (item.Length > offset)
            {
                var spread = item[offset].ToLength();

                if (spread.HasValue)
                {
                    spreadRadius = spread.Value;
                    offset++;
                }
            }

            if (item.Length > offset)
            {
                var col = item[offset].ToColor();

                if (col.HasValue)
                {
                    color = col.Value;
                    offset++;
                }
            }

            if (item.Length > offset)
                return null;

            return new Shadow(inset, offsetX.Value, offsetY.Value, blurRadius, spreadRadius, color);
        }

        #endregion
    }
}
