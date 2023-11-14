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
        public TransformableD065 Node0 => Nodes[0].FlaggedNode as TransformableD065;
        public FlaggedNode Node0_Child => Node0.Children.First() as FlaggedNode;
        public FlaggedNode Node0_D065 => Node0.Children.First() as TransformableD065;

        public PartHeader() : base() =>
            Type = ModelType.Part;

        public PartKind GetKind()
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

        public override bool HasExtraAlignment(FlaggedNode fn, Graph g)
        {
            if (GetKind() == PartKind.Unk_D065_Shatter)
                if (Node0_D065.Children.Skip(1).Contains(fn))
                    return true;
            return false;
        }

        public override bool HasExtraAlignment(Animation n, Graph g) =>
            GetKind() == PartKind.Unk_D065_Shatter && n == Animations.First();
    }
}
