﻿namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Xml;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a number of methods for performing operations that are independent of any particular instance of the DOM.
    /// </summary>
    sealed class DomImplementation : IImplementation
    {
        #region Fields

        readonly Dictionary<String, String[]> _features;

        #endregion

        #region Singleton

        private DomImplementation()
        {
            _features = new Dictionary<String, String[]>(StringComparer.OrdinalIgnoreCase);
            AddFeature("XML", "1.0", "2.0");
            AddFeature("HTML", "1.0", "2.0");
            AddFeature("Core", "2.0");
            AddFeature("Views", "2.0");
            AddFeature("StyleSheets", "2.0");
            AddFeature("CSS", "2.0");
            AddFeature("CSS2", "2.0");
            AddFeature("Traversal", "2.0");
            //Events 2.0
            //UIEvents 2.0
            //MutationEvents 2.0
            //HTMLEvents 2.0
            //Range 2.0
        }

        void AddFeature(String feature, params String[] versions)
        {
            _features.Add(feature, versions);
        }

        public static readonly DomImplementation Instance = new DomImplementation();

        #endregion

        #region Methods

        /// <summary>
        /// Creates an empty DocumentType node. Entity declarations and notations are not made available.
        /// Entity reference expansions and default attribute additions do not occur.
        /// </summary>
        /// <param name="qualifiedName">The qualified name of the document type to be created.</param>
        /// <param name="publicId">The external subset public identifier.</param>
        /// <param name="systemId">The external subset system identifier.</param>
        /// <returns>A new DocumentType node with the owner document set to null.</returns>
        public IDocumentType CreateDocumentType(String qualifiedName, String publicId, String systemId)
        {
            return new DocumentType(qualifiedName) { PublicIdentifier = publicId, SystemIdentifier = systemId };
        }

        /// <summary>
        /// Creates a DOM Document object of the specified type with its document element.
        /// </summary>
        /// <param name="namespaceUri">Optional: The namespace URI of the document element to create.</param>
        /// <param name="qualifiedName">Optional: The qualified name of the document element to be created.</param>
        /// <param name="doctype">Optional: The type of document to be created.</param>
        /// <returns>A new Document object with its document element.</returns>
        public IXmlDocument CreateDocument(String namespaceUri = null, String qualifiedName = null, IDocumentType doctype = null)
        {
            var doc = new XmlDocument();

            if (doctype != null)
                doc.AppendChild(doctype as Node);

            //doc.NodeName = qualifiedName ?? doc.NodeName;
            return doc;
        }

        /// <summary>
        /// Creates a DOM HTML Document object of the specified type with its document element.
        /// </summary>
        /// <param name="title">The title of the HTML document.</param>
        /// <returns>A new Document object with its document element.</returns>
        public IDocument CreateHtmlDocument(String title)
        {
            var doc = new Document();
            doc.Title = title;
            return doc;
        }

        /// <summary>
        /// Test if the DOM implementation implements a specific feature and version, as specified in DOM Features.
        /// </summary>
        /// <param name="feature">The name of the feature requested. Note that any plus sign "+" prepended to the name
        /// of the feature will be ignored since it is not significant in the context of this method.</param>
        /// <param name="version">This is the version number of the feature to test.</param>
        /// <returns>True if the feature is implemented in the specified version, false otherwise.</returns>
        public Boolean HasFeature(String feature, String version = null)
        {
            version = version ?? String.Empty;
            String[] versions;

            if (_features.TryGetValue(feature, out versions))
                return versions.Contains(version, StringComparison.OrdinalIgnoreCase);

            return false;
        }

        #endregion
    }
}
