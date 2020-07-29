using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace MobileCaller.XML
{
    public static class XmlDocumentExtensions
    {
        #region Methods

        public static void Load(this XmlDocument document, StreamReader reader, XmlSchemaToken schemaToken)
        {
            var schemaSet = new XmlSchemaSet();

            XmlReader schemaReader = schemaToken.Assembly.GetEmbeddedXmlData(schemaToken.RelativeResourcePath);
            schemaSet.Add(schemaToken.Namespace, schemaReader);

            var errors = new List<XmlSchemaException>();

            var validationSettings = new XmlReaderSettings();
            validationSettings.Schemas.Add(schemaSet);
            validationSettings.ValidationType = ValidationType.Schema;
            validationSettings.ValidationEventHandler += delegate(object sender, ValidationEventArgs e)
            {
                switch (e.Severity)
                {
                    case XmlSeverityType.Error:
                        errors.Add(e.Exception);
                       break;
                    case XmlSeverityType.Warning:
                        break;
                }
            };

            document.Load(XmlReader.Create(reader, validationSettings));

            if (errors.Count != 0)
                Logger.Write(errors);
        }

        public static XmlReader GetEmbeddedXmlData(this Assembly assembly, string relativeResourcePath)
        {
            string resourcePath = String.Format("{0}.{1}", Regex.Replace(assembly.ManifestModule.Name, @"\.(?:(?:exe)|(?:dll))$", string.Empty, RegexOptions.IgnoreCase), relativeResourcePath);
            Stream stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new ArgumentException(string.Format("The specified embedded resource \"{0}\" is null.", relativeResourcePath));

            return new XmlTextReader(stream);
        }

        #endregion
    }
}
