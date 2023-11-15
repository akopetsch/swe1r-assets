// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.IO.Extensions;
using SWE1R.Assets.Blocks.Common;
using System.Diagnostics;
using System.Linq;

namespace SWE1R.Assets.Blocks
{
    #region Class (base)

    [DebuggerDisplay("Index = {Index,nq}")]
    public abstract class BlockItem
    {
        #region Properties

        public BlockItemPart[] Parts { get; }
        public byte[] Bytes => Parts.SelectMany(p => p.Bytes).ToArray();
        public byte[] Hash => Parts.SelectMany(p => p.Hash).ToArray().GetSha1();
        public string HashString => Hash.ToHexString();
        
        public IBlock Block { get; set; }
        public int? Index => Block?.IndexOf(this);

        #endregion

        #region Constructor

        protected BlockItem(BlockItemPart part) : 
            this(new BlockItemPart[] { part }) { }

        protected BlockItem(BlockItem source) : 
            this(source.Parts.Select(p => p.Clone()).ToArray()) { }

        protected BlockItem(params BlockItemPart[] parts)
        {
            Parts = parts;
            foreach (BlockItemPart p in Parts)
                p.Item = this;
        }

        #endregion

        #region Methods

        public void Load() => 
            Load(out ByteSerializerContext _);

        public abstract void Load(out ByteSerializerContext context);

        public abstract void Unload();

        public void Save() => 
            Save(out ByteSerializerContext _);

        public abstract void Save(out ByteSerializerContext context);

        public abstract BlockItem Clone();

        public static string GetIndexString(int index) => $"{index:d4}";

        #endregion
    }

    #endregion

    #region Classes (generic)

    public abstract class BlockItem<TPart> : BlockItem
        where TPart : BlockItemPart, new()
    {
        #region Properties

        public TPart Part { get; }

        #endregion

        #region Constructor

        protected BlockItem() : base(
            new TPart())
        {
            Part = (TPart)Parts[0];
        }
        protected BlockItem(BlockItem<TPart> copySource) : base(copySource) { }

        #endregion
    }

    public abstract class BlockItem<TPart1, TPart2> : BlockItem
        where TPart1 : BlockItemPart, new()
        where TPart2 : BlockItemPart, new()

    {
        #region Properties

        public TPart1 Part1 { get; }
        public TPart2 Part2 { get; }

        #endregion

        #region Constructor

        protected BlockItem() : base(
            new TPart1(), 
            new TPart2())
        {
            Part1 = (TPart1)Parts[0];
            Part2 = (TPart2)Parts[1];
        }

        protected BlockItem(BlockItem<TPart1, TPart2> copySource) : base(copySource) { }

        #endregion
    }

    #endregion
}
