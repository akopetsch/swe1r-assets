// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.IO;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity
{
    public class TestTextureHelper
    {
        public Texture2D LoadTexture()
        {
            var result = new Texture2D(512, 512);
            result.LoadImage(File.ReadAllBytes(Path.Combine("Assets", "Textures", "TestTexture.png")));
            return result;
        }
    }
}
