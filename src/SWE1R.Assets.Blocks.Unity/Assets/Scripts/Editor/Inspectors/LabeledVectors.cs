// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Editor.Inspectors
{
    public class LabeledVectors : List<LabeledVector>
    {
        public void AddLabel(Vector3 vector, string label) =>
            Get(vector).AddLine(label);

        public LabeledVector Get(Vector3 vector)
        {
            LabeledVector lv = this.FirstOrDefault(x => x.Vector == vector);
            if (lv == null)
                Add(lv = new LabeledVector(vector));
            return lv;
        }
    }
}
