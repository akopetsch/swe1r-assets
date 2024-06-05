// SPDX-License-Identifier: MIT

using System.Xml.Linq;

namespace SWE1R.Assets.Blocks.XmlDocumentation.Tests.ElementValidation
{
    public abstract class ElementValidator(XElement xElement)
    {
        public XElement XElement { get; } = xElement;

        public abstract void Validate();
    }
}
