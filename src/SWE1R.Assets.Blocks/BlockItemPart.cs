// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Linq;

namespace SWE1R.Assets.Blocks
{
    public abstract class BlockItemPart
    {
        public event EventHandler Loaded;

        public byte[] Bytes { get; set; }
        public int Length => Bytes.Length;
        public byte[] Hash => Bytes.GetSha1();

        public BlockItem Item { get; internal set; }
        public int? Index => Array.IndexOf(Item.Parts, this);

        protected BlockItemPart() { }
        protected BlockItemPart(BlockItemPart source) { Load(source.Bytes); }

        public void Load(byte[] bytes)
        {
            Bytes = bytes.ToArray();
            Loaded?.Invoke(this, EventArgs.Empty);
        }

        public abstract BlockItemPart Clone();
    }
}
