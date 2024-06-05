// SPDX-License-Identifier: MIT

using System.Reflection;
using System.Xml.Linq;

namespace CSharpXmlDocumentation
{
    public static class CSharpXmlDocumentationHelper
    {
        private static XDocument Load(Assembly assembly) =>
            XDocument.Load(Path.ChangeExtension(assembly.Location, ".xml"));

        public static XElement? GetSummary(Type type) =>
            Load(type.Assembly)
            .Descendants("member")
            .FirstOrDefault(x => x.Attribute("name")?.Value == $"T:{type.FullName}")?
            .Element(CSharpXmlDocumentationTags.Summary);
    }
}
