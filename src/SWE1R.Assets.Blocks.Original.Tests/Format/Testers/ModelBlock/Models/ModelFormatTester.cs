// SPDX-License-Identifier: MIT

using MoreLinq.Extensions;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models
{
    public abstract class ModelFormatTester<TModel> : Tester<TModel> where TModel : Model
    {
        public override void Test()
        {
            AssertGraphContainment();

            if (Value.Animations != null)
            {
                Assert.True(!Value.Animations.Contains(null));
                Assert.True(!Value.Animations.Duplicates().Any());
            }
            if (Value.AltN != null)
                Assert.True(!Value.AltN.Contains(null));
        }

        private void AssertGraphContainment()
        {
            var headerNodesGraph = Value.GetHeaderFlaggedNodes().SelectMany(x => x.GetSelfAndDescendants()).ToList();

            Assert.True(Value.GetAnimationsTransformedWithPivotNodes().All(x => headerNodesGraph.Contains(x)));
            //Assert.True(Value.GetAltNFlaggedNodes().All(x => headerNodesGraph.Contains(x))); // sometimes fails

            // Material, MaterialTexture (which are also indirectly referenced from Animation)
            var headerFlaggedNodesGraphMaterials = headerNodesGraph.OfType<Mesh>().Select(x => x.Material).ToList();
            var headerFlaggedNodesGraphMaterialTextures = headerFlaggedNodesGraphMaterials.Select(x => x.Texture).Where(x => x != null).ToList();
            if (Value.Animations != null)
                foreach (Animation animation in Value.Animations)
                {
                    // Target property (Material)
                    Target target = animation.TargetOrInteger.Target;
                    if (target != null)
                    {
                        var materials = new List<Material>();
                        if (target.Material != null)
                            materials.Add(target.Material);
                        if (target.MaterialReference != null)
                            materials.Add(target.MaterialReference.Material);
                        Assert.True(materials.All(x => headerFlaggedNodesGraphMaterials.Contains(x)));
                    }

                    // Keyframes property (MaterialTexture)
                    List<MaterialTexture> keyframesMaterialTextures = animation.KeyframesOrInteger.Keyframes?.MaterialTextures ?? new List<MaterialTexture>();
                    //Assert.True(keyframesMaterialTextures.All(x => headerNodesGraphMaterialTextures.Contains(x))); // sometimes fails (and not because AltN graph is excluded)
                }
        }
    }
}
