// SPDX-License-Identifier: MIT

using System;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MacroNameAttribute : Attribute
    {
        public string Value { get; }

        public MacroNameAttribute(string value) =>
            Value = value;
    }
}
