// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine.Mods
{
    public class ModModelVertexAlpha
    {
        public string Filename { get; }
        public int ModelIndex { get; }

        public ModModelVertexAlpha(string filename, int modelIndex)
        {
            Filename = filename;
            ModelIndex = modelIndex;
        }

        public void Run()
        {
            Debug.WriteLine(ModelIndex);

            // load
            Block<ModelBlockItem> block = Block.Load<ModelBlockItem>(Filename);
            ModelBlockItem modelBlockItem = block[ModelIndex];
            modelBlockItem.Load();

            // mod
            SetAlphaTo128(modelBlockItem);

            // save
            modelBlockItem.Save();
            block.Save(Filename);
        }

        private void SetAlphaTo128(ModelBlockItem modelBlockItem)
        {
            List<FlaggedNode> headerFlaggedNodes = modelBlockItem.Header.Nodes
                .Select(x => x.FlaggedNode).Where(x => x != null).Distinct().ToList();
            List<MeshGroup3064> meshGroups = headerFlaggedNodes // TODO: get leaves from animations or altn
                .SelectMany(x => x.GetLeaves()).Distinct().OfType<MeshGroup3064>().ToList();
            List<Mesh> meshes = meshGroups
                .SelectMany(x => x.Children.Cast<Mesh>()).ToList();

            foreach (Mesh mesh in meshes)
            {
                foreach (Vertex vertex in mesh.VisibleVertices)
                {
                    vertex.Byte_F = byte.MaxValue;
                }
            }
        }
    }
}
