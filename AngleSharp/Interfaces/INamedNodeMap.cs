﻿namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a named collection of nodes.
    /// </summary>
    [DOM("NamedNodeMap")]
    public interface INamedNodeMap : IEnumerable<INode>
    {
        /// <summary>
        /// Gets the value of an attribute within the collection.
        /// </summary>
        /// <param name="name">The case-insensitive name.</param>
        /// <returns>The value of the Node or null if it does not exist.</returns>
        [DOM("getNamedItem")]
        INode this[String name] { get; }

        /// <summary>
        /// Adds a node by its localName and namespaceURI.
        /// </summary>
        /// <param name="item">The node to be added or inserted.</param>
        /// <returns>The added node.</returns>
        /// <exception cref="DOMException"></exception>
        [DOM("setNamedItem")]
        INode Add(INode item);

        /// <summary>
        /// Removes a node.
        /// </summary>
        /// <param name="name">The name of the node.</param>
        /// <returns>The removed node or null if nothing has been found.</returns>
        /// <exception cref="DOMException"></exception>
        [DOM("removeNamedItem")]
        INode Remove(String name);

        /// <summary>
        /// Gets the item at the given index.
        /// </summary>
        /// <param name="index">The index of the item to get.</param>
        /// <returns>The item or null if the index is higher or equal to the number of nodes.</returns>
        [DOM("item")]
        INode this[Int32 index] { get; }

        /// <summary>
        /// Gets the number of elements.
        /// </summary>
        [DOM("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets a node by name.
        /// </summary>
        /// <param name="namespaceUri">The namespace of the node.</param>
        /// <param name="localName">The name of the node.</param>
        /// <returns>The node or null if nothing found.</returns>
        /// <exception cref="DOMException"></exception>
        [DOM("getNamedItemNS")]
        INode this[String namespaceUri, String localName] { get; }

        /// <summary>
        /// Adds a node by its localName and namespaceURI.
        /// </summary>
        /// <param name="item">The node to be added or inserted.</param>
        /// <returns>The added node.</returns>
        /// <exception cref="DOMException"></exception>
        [DOM("setNamedItemNS")]
        INode AddWithNamespace(INode item);

        /// <summary>
        /// Removes a node.
        /// </summary>
        /// <param name="namespaceUri">The namespace of the node.</param>
        /// <param name="localName">The name of the node.</param>
        /// <returns>The removed node or null if nothing found.</returns>
        /// <exception cref="DOMException"></exception>
        [DOM("removeNamedItemNS")]
        INode RemoveWithNamespace(String namespaceUri, String localName);
    }
}