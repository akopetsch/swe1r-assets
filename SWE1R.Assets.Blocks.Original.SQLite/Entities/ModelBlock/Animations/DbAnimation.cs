// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE1R.Assets.Blocks.Original.SQLite.Entities.ModelBlock.Anims
{
    [Table("Model_Anim")]
    public class DbAnimation : DbModelStructure<Animation>
    {
        public float Float_0f4 { get; set; }
        public float Float_0f8 { get; set; }
        public float Float_0fc { get; set; }
        public int Bitmask { get; set; }
        public int FramesCount { get; set; }
        public float Float_108 { get; set; }
        public float Float_10c { get; set; }
        public float Float_110 { get; set; }
        public int Int114 { get; set; }
        public int Int118 { get; set; }
        public int P_Timestamps { get; set; }
        public int I_Sub1 { get; set; }
        public int I_Sub2 { get; set; }
        public int Int_128 { get; set; }

        public override void CopyFrom(Node node)
        {
            base.CopyFrom(node);

            var a = (Animation)node.Value;

            Float_0f4 = a.Float_0f4;
            Float_0f8 = a.Float_0f8;
            Float_0fc = a.Float_0fc;
            Bitmask = a.Bitmask;
            FramesCount = a.FramesCount;
            Float_0f4 = a.Float_0f4;
            Float_0f8 = a.Float_0f8;
            Float_0fc = a.Float_0fc;
            Int114 = a.Int114;
            Int118 = a.Int118;
            P_Timestamps = GetPropertyPointer(node, nameof(a.KeyframeTimestamps));
            I_Sub1 = GetKeyframesPointerOrInteger(node);
            I_Sub2 = GetTargetPointerOrInteger(node);
            Int_128 = a.Int_128;
        }

        private static int GetKeyframesPointerOrInteger(Node node)
        {
            var animation = (Animation)node.Value;
            return animation.KeyframesOrInteger.Integer ?? 
                GetValuePosition(node.Graph, animation.KeyframesOrInteger.Keyframes.Value);
        }

        private static int GetTargetPointerOrInteger(Node node)
        {
            var animation = (Animation)node.Value;
            return animation.TargetOrInteger.Integer ??
                GetValuePosition(node.Graph, animation.TargetOrInteger.Target.Value);
        }

        public override bool Equals(DbModelStructure<Animation> other)
        {
            var _other = (DbAnimation)other;

            if (!base.Equals(_other))
                return false;

            if (Float_0f4 != _other.Float_0f4) return false;
            if (Float_0f8 != _other.Float_0f8) return false;
            if (Float_0fc != _other.Float_0fc) return false;
            if (Bitmask != _other.Bitmask) return false;
            if (FramesCount != _other.FramesCount) return false;
            if (Float_108 != _other.Float_108) return false;
            if (Float_10c != _other.Float_10c) return false;
            if (Float_110 != _other.Float_110) return false;
            if (Int114 != _other.Int114) return false;
            if (Int118 != _other.Int118) return false;
            if (P_Timestamps != _other.P_Timestamps) return false;
            if (I_Sub1 != _other.I_Sub1) return false;
            if (I_Sub2 != _other.I_Sub2) return false;
            if (Int_128 != _other.Int_128) return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is DbAnimation)
                return this.Equals((DbAnimation) obj);
            else
                return base.Equals(obj);
        }

        public override int GetHashCode() =>
            HashCode.Combine(base.GetHashCode(),
                HashCode.Combine(Float_0f4, Float_0f8, Float_0fc, Bitmask, FramesCount, Float_108, Float_10c, Float_110),
                HashCode.Combine(Int114, Int118, P_Timestamps, I_Sub1, I_Sub2, Int_128));
    }
}
