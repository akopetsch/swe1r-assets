// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using UnityEngine;

using Swe1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;
using UnityMatrix4x4 = UnityEngine.Matrix4x4;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Nodes
{
    public abstract class FlaggedNodeComponent : MonoBehaviour
    {
        #region Fields

        public int bitfield1;
        public int bitfield2;
        public short number;
        public short padding1;
        public int padding2;

        #endregion

        #region Methods

        public abstract void Import(Swe1rFlaggedNode source);

        public abstract Swe1rFlaggedNode Export(ModelExporter modelExporter);

        #endregion
    }

    public abstract class FlaggedNodeComponent<T> : FlaggedNodeComponent 
        where T : Swe1rFlaggedNode, new()
    {
        public override void Import(Swe1rFlaggedNode source) =>
            Import((T)source);

        public virtual void Import(T source)
        {
            bitfield1 = source.Bitfield1;
            bitfield2 = source.Bitfield2;
            number = source.Number;
            padding1 = source.Padding1;
            padding2 = source.Padding2;
        }

        public override Swe1rFlaggedNode Export(ModelExporter modelExporter)
        {
            var result = new T();

            result.Bitfield1 = bitfield1;
            result.Bitfield2 = bitfield2;
            result.Number = number;
            result.Padding1 = padding1;
            result.Padding2 = padding2;

            return result;
        }

        protected void ApplyMatrix(UnityMatrix4x4 matrix)
        {
            if (!matrix.ValidTRS())
                Debug.LogWarning("invalid RTS");
            else
            {
                //transform.localPosition = matrix.MultiplyPoint3x4(Vector3.one); // MultiplyVector?
                // TODO: Matrix application
                transform.FromMatrix(matrix);
            }
        }
    }
}
