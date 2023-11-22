// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class PartHeader : Header
    {
        #region Properties (helper)

        public TransformableD065 Node0 => Nodes[0].FlaggedNode as TransformableD065;
        public FlaggedNode Node0_Child => Node0.Children.First() as FlaggedNode;
        public FlaggedNode Node0_D065 => Node0.Children.First() as TransformableD065;

        public PartKind Kind
        {
            get
            {
                if (Nodes.Count == 2)
                {
                    return PartKind.RacerLod1;
                }
                else
                {
                    if (Node0_D065 != null)
                        if (Node0_D065.Children.Count == 8)
                            return PartKind.Unk_D065_Shatter;
                        else
                            return PartKind.Unk_D065;
                    else
                        return PartKind.Other;
                }
            }
        }

        #endregion

        #region Constructor

        public PartHeader() : base() =>
            Type = ModelType.Part;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode fn, Graph g)
        {
            if (Kind == PartKind.Unk_D065_Shatter)
                if (Node0_D065.Children.Skip(1).Contains(fn))
                    return true;
            return false;
        }

        public override bool HasExtraAlignment(Animation n, Graph g) =>
            Kind == PartKind.Unk_D065_Shatter && n == Animations.First();

        #endregion
    }
}
