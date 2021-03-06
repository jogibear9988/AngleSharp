﻿namespace AngleSharp.DOM.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a CSS Stylesheet.
    /// </summary>
    sealed class CSSStyleSheet : StyleSheet, ICssStyleSheet, ICssObject
    {
        #region Fields

        readonly CSSRuleList _rules;

        ICssRule _ownerRule;
        IConfiguration _options;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        internal CSSStyleSheet()
        {
            _rules = new CSSRuleList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the style sheet.
        /// </summary>
        public ICssRuleList Rules
        {
            get { return _rules; }
        }

        /// <summary>
        /// Gets the @import rule if the stylesheet was importated otherwise it returns null.
        /// </summary>
        public ICssRule OwnerRule
        {
            get { return _ownerRule; }
            internal set { _ownerRule = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes a style rule from the current style sheet object.
        /// </summary>
        /// <param name="index">The index representing the position to be removed.</param>
        /// <returns>The current stylesheet.</returns>
        public void RemoveAt(Int32 index)
        {
            if (index >= 0 && index < _rules.Length)
                _rules.List.RemoveAt(index);
        }

        /// <summary>
        /// Inserts a new style rule into the current style sheet.
        /// </summary>
        /// <param name="rule">A string containing the rule to be inserted (selector and declaration).</param>
        /// <param name="index">The index representing the position to be inserted.</param>
        /// <returns>The current stylesheet.</returns>
        public Int32 Insert(String rule, Int32 index)
        {
            if (index >= 0 && index <= _rules.Length)
            {
                var value = CssParser.ParseRule(rule) as CSSRule;

                if (value is CSSCharsetRule)
                    throw new DomException(ErrorCode.Syntax);
                else if (value is CSSNamespaceRule && _rules.Any(m => (m is CSSImportRule || m is CSSCharsetRule || m is CSSNamespaceRule) == false))
                    throw new DomException(ErrorCode.InvalidState);

                _rules.List.Insert(index, value);
                return index;
            }
            
            throw new DomException(ErrorCode.IndexSizeError);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            var sb = Pool.NewStringBuilder();

            foreach (var rule in _rules)
                sb.AppendLine(rule.ToCss());

            return sb.ToPool();
        }

        #endregion

        #region Internal Methods

        internal void AddRule(CSSRule rule)
        {
            _rules.List.Add(rule);
        }

        #endregion

        #region Internal Properties

        internal IConfiguration Options
        {
            get { return _options ?? Configuration.Default; }
            set { _options = value; }
        }

        #endregion
    }
}
