// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Models
{
    public abstract class ModelFormatTester<TModel> : Tester<TModel> where TModel : Model
    {
        public override void Test()
        {
            AssertGraphContainment();

            if (Value.Animations != null)
            {
                // Anims does not contain null
                Assert.True(!Value.Animations.Contains(null));

                // Anims only contains distinct values
                Assert.True(Value.Animations.AllUnique());
            }
            if (Value.AltN != null)
                // AltN does not contain null
                Assert.True(!Value.AltN.Contains(null));
        }

        private void AssertGraphContainment()
        {
            List<INode> headerNodesGraph = Value.GetHeaderFlaggedNodes().SelectMany(x => x.GetSelfAndDescendants()).ToList();

            Assert.True(Value.GetAnimationsTransformableD065s().All(x => headerNodesGraph.Contains(x)));
            //Assert.True(Value.GetAltNFlaggedNodes().All(x => headerNodesGraph.Contains(x))); // sometimes fails

            // Material, MaterialTexture (which are also indirectly referenced from Animation)
            var headerNodesGraphMaterials = headerNodesGraph.OfType<Mesh>().Select(x => x.Material).ToList();
            var headerNodesGraphMaterialTextures = headerNodesGraphMaterials.Select(x => x.Texture).Where(x => x != null).ToList();
            if (Value.Animations != null)
            {
                foreach (Animation animation in Value.Animations)
                {
                    // Target property (Material)
                    Target target = animation.TargetOrInteger.Target;
                    if (target != null)
                    {
                        var materials = new List<Material>();
                        if (target.Material != null)
                            materials.Add(target.Material);
                        if (target.DoubleMaterial != null)
                            materials.AddRange(target.DoubleMaterial.GetMaterials().Where(x => x != null));
                        Assert.True(materials.All(x => headerNodesGraphMaterials.Contains(x)));
                    }

                    // Keyframes property (MaterialTexture)
                    List<MaterialTexture> keyframesMaterialTextures = animation.KeyframesOrInteger.Keyframes?.MaterialTextures ?? new List<MaterialTexture>();
                    //Assert.True(keyframesMaterialTextures.All(x => headerNodesGraphMaterialTextures.Contains(x))); // sometimes fails (and not because AltN graph is excluded)
                }
            }
        }
    }
}
