// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class NullComponent : MonoBehaviour
    {
        public void Import()
        {
            gameObject.name = "null";
            gameObject.SetActive(false);
        }
    }
}
