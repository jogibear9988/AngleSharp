﻿namespace AngleSharp.Xml
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Useful helpers for the XML parser.
    /// </summary>
    static class XmlExtensions
    {
        /// <summary>
        /// Determines if the given character is a legal character for the public id field:
        /// http://www.w3.org/TR/REC-xml/#NT-PubidChar
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsPubidChar(this Char c)
        {
            return c.IsAlphanumericAscii() || c == Specification.Minus || c == Specification.SingleQuote || c == Specification.Plus ||
                   c == Specification.Comma || c == Specification.Dot || c == Specification.Solidus || c == Specification.Colon ||
                   c == Specification.QuestionMark || c == Specification.Equality || c == Specification.ExclamationMark || c == Specification.Asterisk ||
                   c == Specification.Num || c == Specification.At || c == Specification.Dollar || c == Specification.Underscore ||
                   c == Specification.RoundBracketOpen || c == Specification.RoundBracketClose || c == Specification.Semicolon || c == Specification.Percent ||
                   c.IsSpaceCharacter();
        }

        /// <summary>
        /// Determines if the given character is a legal name start character for XML.
        /// http://www.w3.org/TR/REC-xml/#NT-NameStartChar
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsXmlNameStart(this Char c)
        {
            return c.IsLetter() || c == Specification.Colon || c == Specification.Underscore || c.IsInRange(0xC0, 0xD6) || 
                   c.IsInRange(0xD8, 0xF6) || c.IsInRange(0xF8, 0x2FF) || c.IsInRange(0x370, 0x37D) || c.IsInRange(0x37F, 0x1FFF) || 
                   c.IsInRange(0x200C, 0x200D) || c.IsInRange(0x2070, 0x218F) || c.IsInRange(0x2C00, 0x2FEF) || 
                   c.IsInRange(0x3001, 0xD7FF) || c.IsInRange(0xF900, 0xFDCF) || c.IsInRange(0xFDF0, 0xFFFD) || 
                   c.IsInRange(0x10000, 0xEFFFF);
        }

        /// <summary>
        /// Determines if the given character is a name character for XML.
        /// http://www.w3.org/TR/REC-xml/#NT-NameChar
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsXmlName(this Char c)
        {
            return c.IsXmlNameStart() || c.IsDigit() || c == Specification.Minus || c == Specification.Dot || c == 0xB7 ||
                   c.IsInRange(0x300, 0x36F) || c.IsInRange(0x203F, 0x2040);
        }

        /// <summary>
        /// Checks if the given char is a valid character.
        /// </summary>
        /// <param name="chr">The char to examine.</param>
        /// <returns>True if the char would indeed be valid.</returns>
        [DebuggerStepThrough]
        public static Boolean IsXmlChar(this Char chr)
        {
            return chr == 0x9 || chr == 0xA || chr == 0xD || (chr >= 0x20 && chr <= 0xD7FF) ||
                    (chr >= 0xE000 && chr <= 0xFFFD);
        }

        /// <summary>
        /// Checks if the given integer would be a valid character.
        /// </summary>
        /// <param name="chr">The integer to examine.</param>
        /// <returns>True if the integer would indeed be valid.</returns>
        [DebuggerStepThrough]
        public static Boolean IsValidAsCharRef(this Int32 chr)
        {
            return  chr == 0x9 || chr == 0xA || chr == 0xD || (chr >= 0x20 && chr <= 0xD7FF) || 
                    (chr >= 0xE000 && chr <= 0xFFFD) || (chr >= 0x10000 && chr <= 0x10FFFF);
        }
    }
}
