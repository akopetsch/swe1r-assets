// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims
{
    [Table($"{nameof(Model)}_{nameof(Animation)}")]
    public class DbAnimation : DbBlockItemStructure<Animation>
    {
        #region Properties

        public float Float_0f4 { get; set; }
        public float Float_0f8 { get; set; }
        public float Float_0fc { get; set; }
        public AnimationType AnimationType { get; set; }
        public AnimationFlags Flags1 { get; set; }
        public int FramesCount { get; set; }
        public float Float_108 { get; set; }
        public float Float_10c { get; set; }
        public float Float_110 { get; set; }
        public int Int114 { get; set; }
        public int Int118 { get; set; }
        public int P_KeyframeTimestamps { get; set; }
        public int I_KeyframesOrInteger { get; set; }
        public int I_TargetPointerOrInteger { get; set; }
        public int Int_128 { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Animation)node.Value;

            Float_0f4 = x.Float_0f4;
            Float_0f8 = x.Float_0f8;
            Float_0fc = x.Float_0fc;
            AnimationType = x.AnimationType;
            Flags1 = x.Flags1;
            FramesCount = x.FramesCount;
            Float_0f4 = x.Float_0f4;
            Float_0f8 = x.Float_0f8;
            Float_0fc = x.Float_0fc;
            Int114 = x.Int114;
            Int118 = x.Int118;
            P_KeyframeTimestamps = GetPropertyPointer(node, nameof(x.KeyframeTimestamps));
            I_KeyframesOrInteger = GetKeyframesPointerOrInteger(node);
            I_TargetPointerOrInteger = GetTargetPointerOrInteger(node);
            Int_128 = x.Int_128;
        }

        private static int GetKeyframesPointerOrInteger(Node node)
        {
            var x = (Animation)node.Value;
            return x.KeyframesOrInteger.Integer ?? 
                GetValuePosition(node.Graph, x.KeyframesOrInteger.Keyframes.Value);
        }

        private static int GetTargetPointerOrInteger(Node node)
        {
            var x = (Animation)node.Value;
            return x.TargetOrInteger.Integer ??
                GetValuePosition(node.Graph, x.TargetOrInteger.Target.Value);
        }

        public override bool Equals(DbBlockItemStructure<Animation> other)
        {
            var x = (DbAnimation)other;

            if (!base.Equals(x))
                return false;

            if (Float_0f4 != x.Float_0f4) return false;
            if (Float_0f8 != x.Float_0f8) return false;
            if (Float_0fc != x.Float_0fc) return false;
            if (AnimationType != x.AnimationType) return false;
            if (Flags1 != x.Flags1) return false;
            if (FramesCount != x.FramesCount) return false;
            if (Float_108 != x.Float_108) return false;
            if (Float_10c != x.Float_10c) return false;
            if (Float_110 != x.Float_110) return false;
            if (Int114 != x.Int114) return false;
            if (Int118 != x.Int118) return false;
            if (P_KeyframeTimestamps != x.P_KeyframeTimestamps) return false;
            if (I_KeyframesOrInteger != x.I_KeyframesOrInteger) return false;
            if (I_TargetPointerOrInteger != x.I_TargetPointerOrInteger) return false;
            if (Int_128 != x.Int_128) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbAnimation x)
                return Equals(x);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            CombineHashCodes(base.GetHashCode(),
                Float_0f4, Float_0f8, Float_0fc, AnimationType, Flags1, FramesCount, Float_108, Float_10c, Float_110,
                Int114, Int118, P_KeyframeTimestamps, I_KeyframesOrInteger, I_TargetPointerOrInteger, Int_128);
    }
}
