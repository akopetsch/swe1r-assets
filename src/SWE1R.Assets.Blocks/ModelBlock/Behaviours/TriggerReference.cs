// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Behaviours
{
    public class TriggerReference
    {
        #region Properties (serialized)

        [Order(0)]
        public int Int_0 { get; set; }
        [Order(1)]
        public int Int_1 { get; set; }
        [Order(2), Reference]
        public TriggerDescription Trigger { get; set; }

        #endregion
    }
}
