// SPDX-License-Identifier: MIT

using CSharpXmlDocumentation;
using System.Xml.Linq;

namespace SWE1R.Assets.Blocks.XmlDocumentation.Tests.ElementValidation.Links
{
    public static class LinkElementValidatorFactory
    {
        public static LinkElementValidator Get(XElement seeElement)
        {
            string href = seeElement.Attribute(CSharpXmlDocumentationAttributeNames.Href).Value;
            var uri = new Uri(href);
            if (uri.Host.Equals("github.com"))
                return new GitHubPermalinkElementValidator(seeElement);
            else
                return new LinkElementValidator(seeElement);
        }
    }
}
