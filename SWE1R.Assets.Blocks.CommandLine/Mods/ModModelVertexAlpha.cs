// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.CommandLine.Mods
{
    public class ModModelVertexAlpha(string filename, int modelIndex)
    {
        public string Filename { get; } = filename;
        public int ModelIndex { get; } = modelIndex;

        public void Run()
        {
            Debug.WriteLine(ModelIndex);

            // load
            var block = Block.Load<ModelBlockItem>(Filename);
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
            var meshes = modelBlockItem.Model.GetAllNodes().OfType<Mesh>().ToList();
            foreach (Mesh mesh in meshes)
                foreach (Vertex vertex in mesh.VisibleVertices)
                    vertex.Byte_F = byte.MaxValue;
        }
    }
}
