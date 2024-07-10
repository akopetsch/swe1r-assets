// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Animations
{
    public static class DecompiledFunctions
    {
        #region Methods

        /// <summary>
        /// <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/Swr/swrModel.c#L698">
        ///   github.com - tim-tim707/SW_RACER_RE - swrModel.c - swrModel_AnimationComputeInterpFactor(...)</see>
        /// </summary>
        public static double swrModel_AnimationComputeInterpFactor(Animation anim, float anim_time, int key_frame_index) =>
            (anim_time - anim.KeyframeTimes[key_frame_index]) / 
            (anim.KeyframeTimes[key_frame_index + 1] - anim.KeyframeTimes[key_frame_index]);

        /// <summary>
        /// <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/Swr/swrModel.c#L728">
        ///   github.com - tim-tim707/SW_RACER_RE - swrModel.c - swrModel_AnimationInterpolateVec3(...)</see>
        /// </summary>
        public static void swrModel_AnimationInterpolateVec3(out Vector3Single result, Animation anim, float time, int key_frame_index)
        {
            float t0 = anim.KeyframeTimes[key_frame_index];
            float t1 = anim.KeyframeTimes[key_frame_index + 1];

            Vector3Single v0 = anim.KeyframesOrInteger.Keyframes.KeyframeTranslations[key_frame_index];
            Vector3Single v1 = anim.KeyframesOrInteger.Keyframes.KeyframeTranslations[key_frame_index + 1];

            if (time >= t1)
                result = v1;
            else if (time <= t0)
                result = v0;
            else
            {
                float t = (float)swrModel_AnimationComputeInterpFactor(anim, time, key_frame_index);
                rdVector_Scale3Add3_both(out result, (1 - t), v0, t, v1);
            }
        }

        /// <summary>
        /// <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/Swr/swrModel.c#L785">
        ///   github.com - tim-tim707/SW_RACER_RE - swrModel.c - swrModel_UpdateTranslationAnimation(...)</see>
        /// </summary>
        public static void swrModel_UpdateTranslationAnimation(Animation anim)
        {
            swrModel_AnimationInterpolateVec3(out Vector3Single result, anim, anim.AnimationTime, anim.KeyframeIndex);
            if (anim.Flags1.HasFlag(AnimationFlags.Transition))
            {
                // lerp result with transition position
                swrModel_AnimationInterpolateVec3(
                    out Vector3Single transition_result,
                    anim,
                    anim.TransitionFromThisAnimationTime,
                    (int)anim.TransitionFromThisKeyframeIndex);
                rdVector_Scale3Add3_both(
                    out result, 
                    (1 - anim.TransitionInterpolationFactor), 
                    transition_result, 
                    anim.TransitionInterpolationFactor, 
                    result);
            }
            AbstractTransformedNode node_ptr = anim.TargetOrInteger.Target.TransformedWithPivotNode;
            if (node_ptr != null)
                swrModel_NodeSetTranslation(node_ptr, result.X, result.Y, result.Z);
        }

        /// <summary>
        /// <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/Swr/swrModel.c#L1255">
        ///   github.com - tim-tim707/SW_RACER_RE - swrModel.c - swrModel_NodeSetTranslation(...)</see>
        /// </summary>
        public static void swrModel_NodeSetTranslation(AbstractTransformedNode node, float x, float y, float z)
        {
            node.Transform.Scale = new Vector3Single(x, y, z);
            node.Flags3 |= 3;
        }

        /// <summary>
        /// <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/Primitives/rdVector.c#L157">
        ///   github.com - tim-tim707/SW_RACER_RE - rdVector.c - rdVector_Scale3Add3_both(...)</see>
        /// </summary>
        public static void rdVector_Scale3Add3_both(out Vector3Single v1, float scale1, Vector3Single v2, float scale2, Vector3Single v3)
        {
            v1 = new Vector3Single
            {
                X = v3.X * scale2 + v2.X * scale1,
                Y = v3.Y * scale2 + v2.Y * scale1,
                Z = v3.Z * scale2 + v2.Z * scale1
            };
        }

        #endregion
    }
}
