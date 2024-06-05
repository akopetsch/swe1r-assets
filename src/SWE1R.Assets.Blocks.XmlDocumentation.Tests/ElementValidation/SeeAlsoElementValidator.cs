// SPDX-License-Identifier: MIT

using CSharpXmlDocumentation;
using SWE1R.Assets.Blocks.XmlDocumentation.Tests.ElementValidation.Links;
using System.Xml.Linq;

namespace SWE1R.Assets.Blocks.XmlDocumentation.Tests.ElementValidation
{
    public class SeeAlsoElementValidator : ElementValidator
    {
        public const string XPath = "./para[starts-with(normalize-space(), 'See also')][1]";

        public LinkElementValidator[] LinkElementValidators { get; }

        public SeeAlsoElementValidator(XElement paraXElement) : 
            base(paraXElement)
        {
            var list = XElement.Element(CSharpXmlDocumentationTags.List);
            LinkElementValidators = list
                .Elements(CSharpXmlDocumentationTags.Item)
                .Elements(LinkElementValidator.Tag)
                .Select(LinkElementValidatorFactory.Get)
                .ToArray();
        }

        public override void Validate()
        {
            foreach (LinkElementValidator linkElementValidator in LinkElementValidators)
                linkElementValidator.Validate();
        }
    }
}
