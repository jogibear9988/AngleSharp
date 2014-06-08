﻿namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// MutationRecord defines an interface that will be passed
    /// to the observer's callback.
    /// </summary>
    [DOM("MutationRecord")]
    interface IMutationRecord
    {
        /// <summary>
        /// Gets attributes if the mutation was an attribute mutation,
        /// characterData if it was a mutation to a CharacterData node,
        /// and childList if it was a mutation to the tree of nodes.
        /// </summary>
        [DOM("type")]
        String Type { get; }

        /// <summary>
        /// Gets the node the mutation affected, depending on the type. For
        /// attributes, it is the element whose attribute changed. For characterData,
        /// it is the CharacterData node. For childList, it is the node whose
        /// children changed.
        /// </summary>
        [DOM("target")]
        INode Target { get; }

        /// <summary>
        /// Gets the nodes added, or null.
        /// </summary>
        [DOM("addedNodes")]
        INodeList AddedNodes { get; }

        /// <summary>
        /// Gets the nodes removed, or null.
        /// </summary>
        [DOM("removedNodes")]
        INodeList RemovedNodes { get; }

        /// <summary>
        /// Gets the previous sibling of the added or removed nodes, or null.
        /// </summary>
        [DOM("previousSibling")]
        INode PreviousSibling { get; }

        /// <summary>
        /// Gets the next sibling of the added or removed nodes, or null.
        /// </summary>
        [DOM("nextSibling")]
        INode NextSibling { get; }

        /// <summary>
        /// Gets the local name of the changed attribute, or null.
        /// </summary>
        [DOM("attributeName")]
        String AttributeName { get; }

        /// <summary>
        /// Gets the namespace of the changed attribute, or null.
        /// </summary>
        [DOM("attributeNamespace")]
        String AttributeNamespace { get; }

        /// <summary>
        /// Gets a string depending on the type. For attributes, it is the value
        /// of the changed attribute before the change. For characterData, it is
        /// the data of the changed node before the change. For childList, it is
        /// null.
        /// </summary>
        [DOM("oldValue")]
        String OldValue { get; }
    }
}