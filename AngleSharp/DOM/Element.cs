﻿namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an element node.
    /// </summary>
    class Element : Node, IElement
    {
        #region Fields

        readonly HtmlElementCollection _elements;
        readonly AttrContainer _attributes;

        String _prefix;
        String _namespace;
        TokenList _classList;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new element node.
        /// </summary>
        internal Element(String name, NodeFlags flags = NodeFlags.None)
            : base(name, NodeType.Element, flags)
        {
            _elements = new HtmlElementCollection(this, deep: false);
            _attributes = new AttrContainer();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the namespace prefix of the specified node, or null if no prefix is specified.
        /// </summary>
        public String Prefix
        {
            get { return _prefix; }
            internal set { _prefix = value; }
        }

        /// <summary>
        /// Gets the local part of the qualified name of this node.
        /// </summary>
        public String LocalName
        {
            get { return NodeName; }
        }

        /// <summary>
        /// Gets the namespace URI of this node.
        /// </summary>
        public String NamespaceUri
        {
            get { return _namespace; }
            internal set { _namespace = value; }
        }

        /// <summary>
        /// Gets or sets the text content of a node and its descendants.
        /// </summary>
        public override String TextContent
        {
            get
            {
                var sb = Pool.NewStringBuilder();

                foreach (var child in ChildNodes)
                {
                    if (child.NodeType != NodeType.Comment && child.NodeType != NodeType.ProcessingInstruction)
                        sb.Append(child.TextContent);
                }

                return sb.ToPool();
            }
            set { base.TextContent = value; }
        }

        /// <summary>
        /// Gets the list of class names.
        /// </summary>
        public ITokenList ClassList
        {
            get { return _classList ?? (_classList = new TokenList(this, AttributeNames.Class)); }
        }

        /// <summary>
        /// Gets or sets the value of the class attribute.
        /// </summary>
        public String ClassName
        {
            get { return GetAttribute(AttributeNames.Class); }
            set { SetAttribute(AttributeNames.Class, value); }
        }

        /// <summary>
        /// Gets or sets the value of the id attribute.
        /// </summary>
        public String Id
        {
            get { return GetAttribute(AttributeNames.Id); }
            set { SetAttribute(AttributeNames.Id, value); }
        }

        /// <summary>
        /// Gets the tagname of the element.
        /// </summary>
        public String TagName
        {
            get { return NodeName; }
        }

        /// <summary>
        /// Gets the element immediately preceding in this node's parent's list of nodes, 
        /// null if the current element is the first element in that list.
        /// </summary>
        public IElement PreviousElementSibling
        {
            get
            {
                var parent = Parent;

                if (parent != null)
                {
                    var found = false;

                    for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
                    {
                        if (parent.ChildNodes[i] == this)
                            found = true;
                        else if (found && parent.ChildNodes[i] is IElement)
                            return (IElement)parent.ChildNodes[i];
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the element immediately following in this node's parent's list of nodes,
        /// or null if the current element is the last element in that list.
        /// </summary>
        public IElement NextElementSibling
        {
            get
            {
                var parent = Parent;

                if (parent != null)
                {
                    var n = parent.ChildNodes.Length;
                    var found = false;

                    for (int i = 0; i < n; i++)
                    {
                        if (parent.ChildNodes[i] == this)
                            found = true;
                        else if (found && parent.ChildNodes[i] is IElement)
                            return (IElement)parent.ChildNodes[i];
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        public Int32 ChildElementCount
        {
            get { return _elements.Length; }
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        public IHtmlCollection Children
        {
            get { return _elements; }
        }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
        public IElement FirstElementChild
        {
            get 
            {
                var children = ChildNodes;
                var n = children.Length;

                for (int i = 0; i < n; i++)
                {
                    var child = children[i] as IElement;

                    if (child != null)
                        return child;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the last child element of this element.
        /// </summary>
        public IElement LastElementChild
        {
            get
            {
                var children = ChildNodes;

                for (int i = children.Length - 1; i >= 0; i--)
                {
                    var child = children[i] as IElement;

                    if (child != null)
                        return child;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the HTML syntax describing the element's descendants.
        /// </summary>
        public String InnerHtml
        {
            get { return ChildNodes.ToHtml(); }
            set
            {
                while (HasChilds)
                    RemoveChild(FirstChild);

                AppendChild(new DocumentFragment(value, this));
            }
        }

        /// <summary>
        /// Gets or sets the HTML syntax describing the element including its descendants. 
        /// </summary>
        public String OuterHtml
        {
            get { return ToHtml(); }
            set
            {
                var parent = Parent;

                if (parent != null)
                {
                    if (Owner != null && Owner.DocumentElement == this)
                        throw new DomException(ErrorCode.NoModificationAllowed);

                    parent.InsertChild(parent.IndexOf(this), new DocumentFragment(value, this));
                    parent.RemoveChild(this);
                }
                else
                    throw new DomException(ErrorCode.NotSupported);
            }
        }
        
        /// <summary>
        /// Gets the sequence of associated attributes.
        /// </summary>
        IEnumerable<IAttr> IElement.Attributes
        {
            get { return _attributes; }
        }

        /// <summary>
        /// Gets the associated attribute container.
        /// </summary>
        internal AttrContainer Attributes
        {
            get { return _attributes; }
        }

        #endregion

        #region Design Properties

        /// <summary>
        /// Gets if the element is being hovered.
        /// </summary>
        [DomName("isHovered")]
        public Boolean IsHovered
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the element has currently focus.
        /// </summary>
        [DomName("isFocused")]
        public Boolean IsFocused
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the width of the left border of this element.
        /// </summary>
        [DomName("clientLeft")]
        public Int32 ClientLeft
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the height of the top border of this element.
        /// </summary>
        [DomName("clientTop")]
        public Int32 ClientTop
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the inner width of this element.
        /// </summary>
        [DomName("clientWidth")]
        public Int32 ClientWidth
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the inner height of this element.
        /// </summary>
        [DomName("clientHeight")]
        public Int32 ClientHeight
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the element from which all offset calculations are currently computed.
        /// </summary>
        [DomName("offsetParent")]
        public Element OffsetParent
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the distance from this element's left border to its offsetParent's left border.
        /// </summary>
        [DomName("offsetLeft")]
        public Int32 OffsetLeft
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the distance from this element's top border to its offsetParent's top border.
        /// </summary>
        [DomName("offsetTop")]
        public Int32 OffsetTop
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the width of this element, relative to the layout.
        /// </summary>
        [DomName("offsetWidth")]
        public Int32 OffsetWidth
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the height of this element, relative to the layout.
        /// </summary>
        [DomName("offsetHeight")]
        public Int32 OffsetHeight
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the left scroll offset of an element.
        /// </summary>
        [DomName("scrollLeft")]
        public Int32 ScrollLeft
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the top scroll offset of an element.
        /// </summary>
        [DomName("scrollTop")]
        public Int32 ScrollTop
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the scroll view width of an element.
        /// </summary>
        [DomName("scrollWidth")]
        public Int32 ScrollWidth
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the scroll view height of an element.
        /// </summary>
        [DomName("scrollHeight")]
        public Int32 ScrollHeight
        {
            get;
            internal set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        public override INode AppendChild(INode child)
        {
            var node = DefaultAppendChild(child);
            OnChildrenChanged();
            return node;
        }

        /// <summary>
        /// Inserts a child to the collection of children at the specified index.
        /// </summary>
        /// <param name="index">The index where to insert.</param>
        /// <param name="child">The child to insert.</param>
        /// <returns>The inserted child.</returns>
        public override INode InsertChild(Int32 index, INode child)
        {
            var node = DefaultInsertChild(index, child);
            OnChildrenChanged();
            return node;
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">The node before which newElement is inserted. If
        /// referenceElement is null, newElement is inserted at the end of the list of child nodes.</param>
        /// <returns>The inserted node.</returns>
        public override INode InsertBefore(INode newElement, INode referenceElement)
        {
            var node = DefaultInsertBefore(newElement, referenceElement);
            OnChildrenChanged();
            return node;
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">The new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">The existing child to be replaced.</param>
        /// <returns>The replaced node. This is the same node as oldChild.</returns>
        public override INode ReplaceChild(INode newChild, INode oldChild)
        {
            var node = DefaultReplaceChild(newChild, oldChild);
            OnChildrenChanged();
            return node;
        }

        /// <summary>
        /// Removes a child from the collection of children.
        /// </summary>
        /// <param name="child">The child to remove.</param>
        /// <returns>The removed child.</returns>
        public override INode RemoveChild(INode child)
        {
            var node = DefaultRemoveChild(child);
            OnChildrenChanged();
            return node;
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        public IElement QuerySelector(String selectors)
        {
            return ChildNodes.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection QuerySelectorAll(String selectors)
        {
            return ChildNodes.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection GetElementsByClassName(String classNames)
        {
            return ChildNodes.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        public IHtmlCollection GetElementsByTagName(String tagName)
        {
            return ChildNodes.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="namespaceURI">The namespace URI of elements to look for.</param>
        /// <param name="tagName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <returns>A NodeList of found elements in the order they appear in the tree.</returns>
        public IHtmlCollection GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return ChildNodes.GetElementsByTagNameNS(namespaceURI, tagName);
        }

        public Boolean Matches(String selectors)
        {
            return AngleSharp.Parser.Css.CssParser.ParseSelector(selectors).Match(this);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new Element(NodeName, Flags);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        public override String LookupNamespaceUri(String prefix)
        {
            if (!String.IsNullOrEmpty(_namespace) && Prefix == prefix)
                return _namespace;

            for (int i = 0; i < _attributes.Count; i++)
            {
                var attr = _attributes[i];

                if ((attr.Prefix == Namespaces.Declaration && attr.LocalName == prefix) || (attr.LocalName == Namespaces.Declaration && prefix == null))
                {
                    if (!String.IsNullOrEmpty(attr.Value))
                        return attr.Value;

                    return null;
                }
            }

            var parent = Parent;

            if (parent != null)
                parent.LookupNamespaceUri(prefix);

            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default
        /// namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element
        /// will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        public override Boolean IsDefaultNamespace(String namespaceURI)
        { 
            if (String.IsNullOrEmpty(Prefix))
                return _namespace == namespaceURI;

            var ns = GetAttribute(Namespaces.Declaration);

            if (!String.IsNullOrEmpty(ns))
                return ns == namespaceURI;

            var parent = Parent;

            if (parent != null)
                return parent.IsDefaultNamespace(namespaceURI);

            return false;
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element has the specified attribute or not.
        /// </summary>
        /// <param name="name">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        public Boolean HasAttribute(String name)
        {
            return _attributes.Has(name);
        }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element has the specified attribute or not.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localName">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        public Boolean HasAttribute(String namespaceUri, String localName)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].LocalName.Equals(localName, StringComparison.OrdinalIgnoreCase) && _attributes[i].NamespaceUri == namespaceUri)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute's value.</returns>
        public String GetAttribute(String name)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return _attributes[i].Value;
            }

            return null;
        }

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localName">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute's value.</returns>
        public String GetAttribute(String namespaceUri, String localName)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].LocalName == localName && _attributes[i].NamespaceUri == namespaceUri)
                    return _attributes[i].Value;
            }

            return null;
        }

        /// <summary>
        /// Adds a new attribute if the attribute is not yet created.
        /// Does not fire the changed event.
        /// </summary>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        internal void AddAttribute(String name, String value)
        {
            if (!_attributes.Has(name))
                _attributes.Add(new Attr(name, value, OnAttributeChanged));
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        public void SetAttribute(String name, String value)
        {
            if (value == null)
            {
                RemoveAttribute(name);
                return;
            }

            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    _attributes[i].Value = value;
                    return;
                }
            }

            _attributes.Add(new Attr(name, value, OnAttributeChanged));
            OnAttributeChanged(name);
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        public void SetAttribute(String namespaceUri, String name, String value)
        {
            if (value == null)
            {
                RemoveAttribute(namespaceUri, name);
                return;
            }

            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    _attributes[i].Value = value;
                    return;
                }
            }

            _attributes.Add(new Attr(name, value, namespaceUri, OnAttributeChanged));
            OnAttributeChanged(name);
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="name">Is a string that names the attribute to be removed.</param>
        /// <returns>The current element.</returns>
        public void RemoveAttribute(String name)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    _attributes.RemoveAt(i);
                    OnAttributeChanged(name);
                    break;
                }
            }
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localName">Is a string that names the attribute to be removed.</param>
        /// <returns>The current element.</returns>
        public void RemoveAttribute(String namespaceUri, String localName)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].LocalName.Equals(localName, StringComparison.OrdinalIgnoreCase) && _attributes[i].NamespaceUri == namespaceUri)
                {
                    var name = _attributes[i].Name;
                    _attributes.RemoveAt(i);
                    OnAttributeChanged(name);
                    break;
                }
            }
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        public override String LookupPrefix(String namespaceURI)
        {
            if (String.IsNullOrEmpty(namespaceURI))
                return null;

            if (!String.IsNullOrEmpty(_namespace) && !String.IsNullOrEmpty(_prefix) && _namespace == namespaceURI && LookupNamespaceUri(Prefix) == namespaceURI)
                return Prefix;

            var parent = Parent;

            if (parent != null)
                return parent.LookupPrefix(namespaceURI);

            return null;
        }

        /// <summary>
        /// Prepends nodes to the current node.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        public void Prepend(params INode[] nodes)
        {
            if (Parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                InsertChild(0, node);
            }
        }

        /// <summary>
        /// Appends nodes to current node.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        public void Append(params INode[] nodes)
        {
            if (Parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                AppendChild(node);
            }
        }

        public override Boolean IsEqualNode(INode otherNode)
        {
            var otherElement = otherNode as IElement;

            if (otherElement != null)
            {
                if (this.NamespaceUri != otherElement.NamespaceUri)
                    return false;

                if (_attributes.Count != otherElement.Attributes.Count())
                    return false;

                for (int i = 0; i < _attributes.Count; i++)
                {
                    if (!otherElement.Attributes.Any(m => m.Name == _attributes[i].Name && m.Value == _attributes[i].Value))
                        return false;
                }

                return base.IsEqualNode(otherNode);
            }

            return false;
        }

        /// <summary>
        /// Inserts nodes before the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert before.</param>
        /// <returns>The current element.</returns>
        public void Before(params INode[] nodes)
        {
            var parent = Parent;

            if (parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                parent.InsertBefore(node, this);
            }
        }

        /// <summary>
        /// Inserts nodes after the current node.
        /// </summary>
        /// <param name="nodes">The nodes to insert after.</param>
        /// <returns>The current element.</returns>
        public void After(params INode[] nodes)
        {
            var parent = Parent;

            if (parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                parent.InsertBefore(node, NextSibling);
            }
        }

        /// <summary>
        /// Replaces the current node with the nodes.
        /// </summary>
        /// <param name="nodes">The nodes to replace.</param>
        public void Replace(params INode[] nodes)
        {
            var parent = Parent;

            if (parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                parent.ReplaceChild(node, this);
            }
        }

        /// <summary>
        /// Removes the current element from the parent.
        /// </summary>
        public void Remove()
        {
            var parent = Parent;

            if (parent != null)
                parent.RemoveChild(this);
        }

        /// <summary>
        /// Inserts new HTML elements specified by the given HTML string at
        /// a position relative to the current element specified by the position.
        /// </summary>
        /// <param name="position">The relation to the current element.</param>
        /// <param name="html">The HTML code to generate elements for.</param>
        public void Insert(AdjacentPosition position, String html)
        {
            var useThis = position == AdjacentPosition.BeforeBegin || position == AdjacentPosition.AfterEnd;
            var nodeParent = useThis ? this : Parent as Element;
            var nodes = new DocumentFragment(html, nodeParent);

            switch (position)
            {
                case AdjacentPosition.BeforeBegin:
                    Parent.InsertBefore(nodes, this);
                    break;

                case AdjacentPosition.AfterEnd:
                    Parent.InsertChild(Parent.IndexOf(this) + 1, nodes);
                    break;

                case AdjacentPosition.AfterBegin:
                    InsertChild(0, nodes);
                    break;

                case AdjacentPosition.BeforeEnd:
                    AppendChild(nodes);
                    break;
            }
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            var sb = Pool.NewStringBuilder();
            var tagName = (Flags & (NodeFlags.HtmlMember | NodeFlags.SvgMember | NodeFlags.MathMember)) != NodeFlags.None ? LocalName : NodeName;

            sb.Append(Specification.LessThan).Append(tagName);

            foreach (var attribute in _attributes)
                sb.Append(Specification.Space).Append(attribute.ToString());

            sb.Append(Specification.GreaterThan);

            if (!Flags.HasFlag(NodeFlags.SelfClosing))
            {
                if (Flags.HasFlag(NodeFlags.LineTolerance) && FirstChild is IText)
                {
                    var text = (IText)FirstChild;

                    if (text.Data.Length > 0 && text.Data[0] == Specification.LineFeed)
                        sb.Append(Specification.LineFeed);
                }

                foreach (var child in ChildNodes)
                    sb.Append(child.ToHtml());

                sb.Append(Specification.LessThan).Append(Specification.Solidus).Append(tagName);
                sb.Append(Specification.GreaterThan);
            }

            return sb.ToPool();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Copies the attributes from the source element to the target element.
        /// Each attribute will be recreated on the target.
        /// </summary>
        /// <param name="source">The source of the attributes.</param>
        /// <param name="target">The target where to create the attributes.</param>
        protected static void CopyAttributes(Element source, Element target)
        {
            target._namespace = source._namespace;
            target._prefix = source._prefix;

            for (int i = 0; i < source._attributes.Count; i++)
                target.SetAttribute(source._attributes[i].Name, source._attributes[i].Value);
        }

        /// <summary>
        /// Called if an attribute changed, has been added or removed.
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        protected virtual void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Class, StringComparison.Ordinal))
            {
                if (_classList != null)
                    _classList.Update(ClassName);
            }
        }

        /// <summary>
        /// Called if the children structure changed (due to add, insert, replace or remove).
        /// </summary>
        protected virtual void OnChildrenChanged()
        {
        }

        #endregion
    }
}
