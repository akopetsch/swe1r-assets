// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Swe1rMappingSub = SWE1R.Assets.Blocks.ModelBlock.Meshes.MappingSub;

namespace SWE1R.Assets.Blocks.Unity.ScriptableObjects
{
    public class MappingSubScriptableObject : AbstractScriptableObject<Swe1rMappingSub>
    {
        public int int_0;
        public int int_1;
        public MappingChildScriptableObject child;

        public override void Import(Swe1rMappingSub source, ModelImporter importer)
        {
            int_0 = source.Int_0;
            int_1 = source.Int_1;
            if (source.Child != null)
                child = importer.GetMappingChildScriptableObject(source.Child);
        }

        public override Swe1rMappingSub Export(ModelExporter exporter)
        {
            var result = new Swe1rMappingSub();
            result.Int_0 = int_0;
            result.Int_1 = int_1;
            if (child != null)
                result.Child = exporter.GetMappingChild(child);
            return result;
        }
    }
}
