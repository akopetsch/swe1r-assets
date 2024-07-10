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

        public float LoopTransitionSpeed { get; set; }
        public float TransitionSpeed { get; set; }
        public float TransitionInterpolationFactor { get; set; }
        public float TransitionFromThisKeyframeIndex { get; set; }
        public float TransitionFromThisAnimationTime { get; set; }
        public float AnimationStartTime { get; set; }
        public float AnimationEndTime { get; set; }
        public float AnimationDuration { get; set; }
        public float Duration3 { get; set; }
        public AnimationType AnimationType { get; set; }
        public AnimationFlags Flags1 { get; set; }
        public uint KeyframesCount { get; set; }
        public float Duration4 { get; set; }
        public float Duration5 { get; set; }
        public float AnimationSpeed { get; set; }
        public float AnimationTime { get; set; }
        public int KeyframeIndex { get; set; }
        public int P_KeyframeTimes { get; set; }
        public int I_KeyframesOrInteger { get; set; }
        public int I_TargetOrInteger { get; set; }
        public uint Unk11 { get; set; }

        #endregion

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var x = (Animation)node.Value;

            LoopTransitionSpeed = x.LoopTransitionSpeed;
            TransitionSpeed = x.TransitionSpeed;
            TransitionInterpolationFactor = x.TransitionInterpolationFactor;
            TransitionFromThisKeyframeIndex = x.TransitionFromThisKeyframeIndex;
            TransitionFromThisAnimationTime = x.TransitionFromThisAnimationTime;
            AnimationStartTime = x.AnimationStartTime;
            AnimationEndTime = x.AnimationEndTime;
            AnimationDuration = x.AnimationDuration;
            Duration3 = x.Duration3;
            AnimationType = x.AnimationType;
            Flags1 = x.Flags1;
            KeyframesCount = x.KeyframesCount;
            Duration4 = x.Duration4;
            Duration5 = x.Duration5;
            AnimationSpeed = x.AnimationSpeed;
            AnimationTime = x.AnimationTime;
            KeyframeIndex = x.KeyframeIndex;
            P_KeyframeTimes = GetPropertyPointer(node, nameof(x.KeyframeTimes));
            I_KeyframesOrInteger = GetKeyframesPointerOrInteger(node);
            I_TargetOrInteger = GetTargetPointerOrInteger(node);
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
            if (Duration4 != x.Duration4) return false;
            if (Duration5 != x.Duration5) return false;
            if (AnimationSpeed != x.AnimationSpeed) return false;
            if (AnimationTime != x.AnimationTime) return false;
            if (KeyframeIndex != x.KeyframeIndex) return false;
            if (P_KeyframeTimes != x.P_KeyframeTimes) return false;
            if (I_KeyframesOrInteger != x.I_KeyframesOrInteger) return false;
            if (I_TargetOrInteger != x.I_TargetOrInteger) return false;
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
                LoopTransitionSpeed,
                TransitionSpeed,
                TransitionInterpolationFactor,
                TransitionFromThisKeyframeIndex,
                TransitionFromThisAnimationTime,
                AnimationStartTime,
                AnimationEndTime,
                AnimationDuration,
                Duration3,
                AnimationType,
                Flags1,
                KeyframesCount,
                Duration4,
                Duration5,
                AnimationSpeed,
                AnimationTime,
                KeyframeIndex,
                P_KeyframeTimes,
                I_KeyframesOrInteger,
                I_TargetOrInteger,
                Unk11);
    }
}
