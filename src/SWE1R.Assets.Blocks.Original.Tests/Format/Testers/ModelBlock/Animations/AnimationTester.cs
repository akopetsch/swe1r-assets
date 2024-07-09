// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock.Animations;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Animations
{
    public class AnimationTester : Tester<Animation>
    {
        public override void Test()
        {
            Test_AnimationType();
            Test_Flags1();
        }

        private void Test_AnimationType() =>
            Assert.Contains(Value.AnimationType, Enum.GetValues<AnimationType>());
        
        private void Test_Flags1()
        {
            uint allEnumValues = (uint)(
                AnimationFlags.Loop |
                AnimationFlags.Unknown_20 |
                AnimationFlags.LoopWithTransition |
                AnimationFlags.Unknown_1000 |
                AnimationFlags.Reset |
                AnimationFlags.Transition |
                AnimationFlags.TransitioningNow |
                AnimationFlags.Enabled |
                AnimationFlags.Disabled
            );

            uint undefinedBits = (uint)Value.Flags1 & ~allEnumValues;
            Assert.Equal(0u, undefinedBits);
        }
    }
}
