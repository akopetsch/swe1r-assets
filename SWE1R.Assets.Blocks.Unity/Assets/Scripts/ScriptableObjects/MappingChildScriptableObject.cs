// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using SWE1R.Assets.Blocks.Unity.Extensions;
using Swe1rMappingChild = SWE1R.Assets.Blocks.ModelBlock.Meshes.MappingChild;
using UnityVector3 = UnityEngine.Vector3;

namespace SWE1R.Assets.Blocks.Unity.ScriptableObjects
{
    public class MappingChildScriptableObject : AbstractScriptableObject<Swe1rMappingChild>
    {
        public UnityVector3 vector_00;
        public UnityVector3 vector_0c;
        public short word_18;
        public byte byte_1a;
        public byte byte_1b;
        public short word_1c;
        public byte byte_1e;
        public byte byte_1f;
        public FlaggedNodeComponent flaggedNode_20;
        public short word_24;
        public short word_26;
        public MappingChildScriptableObject next;

        public override void Import(Swe1rMappingChild source, ModelImporter importer)
        {
            vector_00 = source.Vector_00.ToUnityVector3();
            vector_0c = source.Vector_0c.ToUnityVector3();
            word_18 = source.Word_18;
            byte_1a = source.Byte_1a;
            byte_1b = source.Byte_1b;
            word_1c = source.Word_1c;
            byte_1e = source.Byte_1e;
            byte_1f = source.Byte_1f;
            word_24 = source.Word_24;
            word_26 = source.Word_26;
            if (source.Next != null)
                next = importer.GetMappingChildScriptableObject(source.Next);
        }

        public void ImportFlaggedNode(Swe1rMappingChild source, ModelImporter importer)
        {
            if (source.FlaggedNode_20 != null)
                flaggedNode_20 = importer.GetFlaggedNodeComponent<FlaggedNodeComponent>(
                    source.FlaggedNode_20);
        }

        public override Swe1rMappingChild Export(ModelExporter exporter)
        {
            var result = new Swe1rMappingChild();
            result.Vector_00 = vector_00.ToSwe1rVector3Single();
            result.Vector_0c = vector_0c.ToSwe1rVector3Single();
            result.Word_18 = word_18;
            result.Byte_1a = byte_1a;
            result.Byte_1b = byte_1b;
            result.Word_1c = word_1c;
            result.Byte_1e = byte_1e;
            result.Byte_1f = byte_1f;
            if (flaggedNode_20 != null)
                result.FlaggedNode_20 = exporter.GetFlaggedNode(flaggedNode_20.gameObject);
            result.Word_24 = word_24;
            result.Word_26 = word_26;
            if (next != null)
                result.Next = exporter.GetMappingChild(next);
            return result;
        }
    }
}
