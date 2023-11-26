// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Attributes.Helpers;
using ByteSerialization.Components.Values;
using ByteSerialization.Components.Values.Composites.Records;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using SWE1R.Assets.Blocks.Vectors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public class CollisionVertices
    {
        #region Properties (serialization)

        [Length(typeof(LengthHelper))]
        [TypeHelper(typeof(TypeHelper))]
        [Order(0)] public object Value { get; private set; } // TODO: IList

        [Padding(alignment: 4)]
        [Order(1)] public byte[] PaddingGarbage { get; set; }

        #endregion

        #region Properties (C union style access)

        public IList List => Value as IList;

        public List<Vector3Int16> ShortVectors
        {
            get => Value as List<Vector3Int16>;
            set => Value = value;
        }

        public List<Vector3Single> FloatVectors
        {
            get => Value as List<Vector3Single>;
            set => Value = value;
        }

        #endregion

        #region Classes (serialization)

        private class LengthHelper : IBindingHelper
        {
            public int GetValue(PropertyComponent list)
            {
                var mesh = list.GetAncestorValue<Mesh>();
                return mesh.CollisionVerticesCount;
            }
        }

        private class TypeHelper : ITypeHelper
        {
            public Type GetPropertyType(RecordComponent recordNode)
            {
                var recordAncestorsTypes = recordNode.GetAncestors<RecordComponent>().Select(rc => rc.Type);
                if (recordAncestorsTypes.SequenceEqual(new Type[]
                {
                    typeof(Mesh),
                    typeof(MeshGroup3064),
                    typeof(TransformableD065),
                    typeof(Group5064),
                    typeof(FlaggedNodeOrInteger),
                    typeof(ModlModel),
                }))
                    // 114, 151
                    // TODO: move this comment
                    return typeof(List<Vector3Single>);
                else
                    return typeof(List<Vector3Int16>);
            }
        }

        #endregion
    }
}
