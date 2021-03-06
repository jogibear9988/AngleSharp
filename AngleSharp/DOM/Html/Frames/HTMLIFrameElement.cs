﻿namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    sealed class HTMLIFrameElement : HTMLFrameElementBase, IHtmlInlineFrameElement
    {
        #region Fields
        ISettableTokenList _sandbox;
        #endregion

        #region ctor

        internal HTMLIFrameElement()
            : base(Tags.Iframe, NodeFlags.LiteralText)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public Alignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(Alignment.Bottom); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets the content of the page that the nested browsing context is to contain.
        /// </summary>
        public String ContentHtml
        {
            get { return GetAttribute(AttributeNames.SrcDoc); }
            set { SetAttribute(AttributeNames.SrcDoc, value); }
        }

        public ISettableTokenList Sandbox
        {
            get { return _sandbox ?? (_sandbox = new SettableTokenList(this, AttributeNames.Sandbox)); }
        }

        /// <summary>
        /// Gets or sets the value of the seamless attribute.
        /// </summary>
        public Boolean IsSeamless
        {
            get { return GetAttribute(AttributeNames.SrcDoc) != null; }
            set { SetAttribute(AttributeNames.SrcDoc, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the frame's content can trigger the fullscreen mode.
        /// </summary>
        public Boolean IsFullscreenAllowed
        {
            get { return GetAttribute(AttributeNames.AllowFullscreen) != null; }
            set { SetAttribute(AttributeNames.AllowFullscreen, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets the frame's parent's window context.
        /// </summary>
        public IWindowProxy ContentWindow
        {
            get { return null; }
        }

        #endregion
    }
}
