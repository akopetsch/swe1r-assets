﻿// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.N64Sdk;
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
                foreach (Vtx vertex in mesh.Vertices)
                    vertex.Byte_F = byte.MaxValue;
        }
    }
}
