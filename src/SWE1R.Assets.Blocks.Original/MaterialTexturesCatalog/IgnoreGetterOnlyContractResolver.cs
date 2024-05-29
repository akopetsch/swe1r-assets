// SPDX-License-Identifier: MIT

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
