// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog
{
    public class IgnoreGetterOnlyContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            // getter-only properties are not serialized
            if (property.Readable && !property.Writable)
                property.ShouldSerialize = instance => false;

            return property;
        }
    }
}
