// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Editor.Inspectors
{
    public class LabeledVector
    {
        public Vector3 Vector { get; }
        public Color Color { get; set; }
        public string Text { get; private set; } = string.Empty;

        public LabeledVector(Vector3 vector) =>
            Vector = vector;

        public void AddLine(string line) =>
            Text += Environment.NewLine + line;
    }
}
