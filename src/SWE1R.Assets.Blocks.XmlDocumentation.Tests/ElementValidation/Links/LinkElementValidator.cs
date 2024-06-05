// SPDX-License-Identifier: MIT

using CSharpXmlDocumentation;
using System.Xml.Linq;

namespace SWE1R.Assets.Blocks.XmlDocumentation.Tests.ElementValidation.Links
{
    public class LinkElementValidator : ElementValidator
    {
        public const string Tag = CSharpXmlDocumentationTags.See;

        public string Href { get; }
        public string Text { get; }
        public string[] TextSplit { get; }

        public LinkElementValidator(XElement seeElement) :
            base(seeElement)
        {
            Href = seeElement.Attribute(CSharpXmlDocumentationAttributeNames.Href).Value;
            Text = seeElement.Value.Trim();
            TextSplit = Text.Split(" - ");
        }

        public override void Validate() =>
            throw new NotImplementedException();
    }
}
