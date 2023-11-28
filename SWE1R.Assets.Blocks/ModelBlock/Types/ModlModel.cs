// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class ModlModel : Model
    {
        #region Properties (helper)

        public TransformableD065 D065 => Nodes[0].FlaggedNode as TransformableD065;
        public Group5065 D065_5065 => D065?.Children[0] as Group5065;
        public Group5064 D065_5065_5064 => D065_5065?.Children.ElementAtOrDefault(1) as Group5064;

        #endregion

        #region Constructor

        public ModlModel() : base() =>
            Type = ModelType.Modl;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode fn, ByteSerializerGraph g) => 
            D065_5065_5064?.Children.Skip(1).Contains(fn) ?? false;

        public override bool HasExtraAlignment(Animation n, ByteSerializerGraph g) => 
            n == Animations?.First();

        #endregion
    }
}
