// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class IntegerComponent : MonoBehaviour
    {
        public int integer;

        public void Import(int integer)
        {
            gameObject.name = integer.ToString("x8");
            this.integer = integer;
        }

        public int Export() =>
            integer;
    }
}
