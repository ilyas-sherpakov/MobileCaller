using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MobileCaller.XML
{
    /// <summary>
    /// Contains information used to find embedded schema to validate XML documents.
    /// </summary>
    public class XmlSchemaToken
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of XmlSchemaToken.
        /// </summary>
        /// <param name="assembly">A reference to the assembly containing the embedded XML schema.</param>
        /// <param name="relativeResourcePath">The relative path to the resource containing the XML schema.</param>
        /// <param name="xmlNamespace">The namespace for the schema.</param>
        public XmlSchemaToken(Assembly assembly, string relativeResourcePath, string xmlNamespace)
        {
            Assembly = assembly;
            RelativeResourcePath = relativeResourcePath;
            Namespace = xmlNamespace;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a reference to the assembly containing the embedded XML schema.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Gets the relative path to the resource containing the XML schema.
        /// </summary>
        public string RelativeResourcePath { get; private set; }

        /// <summary>
        /// Gets the namespace for the XML schema.</summary>
        public string Namespace { get; private set; }

        #endregion
    }
}
