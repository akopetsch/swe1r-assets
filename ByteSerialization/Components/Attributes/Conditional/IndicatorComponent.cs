// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Attributes.Conditional;
using ByteSerialization.Nodes;
using System.Linq;

namespace ByteSerialization.Attributes.Conditional
{
    public class IndicatorComponent : AttributeComponent<IndicatorAttribute>, IConditionalComponent
    {
        #region Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Node.OnSerializing += ApplyIndicator;
        }

        private void ApplyIndicator()
        {
            if (Value != null)
                Writer.Write(Attribute.Value.ToCharArray());
        }

        public bool IsSerialized(Node node)
        {
            // get target indicator
            var target = Attribute.Value;

            // get actual indicator
            int n = target.Length;
            byte[] bytes = Reader.ReadBytes(n);
            char[] chars = bytes.Select(b => (char)b).ToArray();
            var actual = new string(chars);

            // if target indicator equals actual indicator...
            if (target.Equals(actual))
                return true;
            else
            {
                Context.Position -= n;
                return false;
            }
        }

        #endregion
    }
}
