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

        public float AnimationEndTime { get; set; }
        public float AnimationDuration { get; set; }
        public float Duration3 { get; set; }
        public AnimationType AnimationType { get; set; }
        public AnimationFlags Flags1 { get; set; }
        public int KeyframesCount { get; set; }
        public float Float_108 { get; set; }
        public float Float_10c { get; set; }
        public float Float_110 { get; set; }
        public int AnimationTime { get; set; }
        public int KeyframeIndex { get; set; }
        public int P_KeyframesTimes { get; set; }
        public int I_KeyframesOrInteger { get; set; }
        public int I_TargetPointerOrInteger { get; set; }
        public int Unk11 { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Animation)node.Value;

            AnimationEndTime = x.AnimationEndTime;
            AnimationDuration = x.AnimationDuration;
            Duration3 = x.Duration3;
            AnimationType = x.AnimationType;
            Flags1 = x.Flags1;
            KeyframesCount = x.KeyframesCount;
            AnimationEndTime = x.AnimationEndTime;
            AnimationDuration = x.AnimationDuration;
            Duration3 = x.Duration3;
            AnimationTime = x.AnimationTime;
            KeyframeIndex = x.KeyframeIndex;
            P_KeyframesTimes = GetPropertyPointer(node, nameof(x.KeyframesTimes));
            I_KeyframesOrInteger = GetKeyframesPointerOrInteger(node);
            I_TargetPointerOrInteger = GetTargetPointerOrInteger(node);
            Unk11 = x.Unk11;
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

            if (AnimationEndTime != x.AnimationEndTime) return false;
            if (AnimationDuration != x.AnimationDuration) return false;
            if (Duration3 != x.Duration3) return false;
            if (AnimationType != x.AnimationType) return false;
            if (Flags1 != x.Flags1) return false;
            if (KeyframesCount != x.KeyframesCount) return false;
            if (Float_108 != x.Float_108) return false;
            if (Float_10c != x.Float_10c) return false;
            if (Float_110 != x.Float_110) return false;
            if (AnimationTime != x.AnimationTime) return false;
            if (KeyframeIndex != x.KeyframeIndex) return false;
            if (P_KeyframesTimes != x.P_KeyframesTimes) return false;
            if (I_KeyframesOrInteger != x.I_KeyframesOrInteger) return false;
            if (I_TargetPointerOrInteger != x.I_TargetPointerOrInteger) return false;
            if (Unk11 != x.Unk11) return false;

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
                AnimationEndTime, AnimationDuration, Duration3, AnimationType, Flags1, KeyframesCount, Float_108, Float_10c, Float_110,
                AnimationTime, KeyframeIndex, P_KeyframesTimes, I_KeyframesOrInteger, I_TargetPointerOrInteger, Unk11);
    }
}
