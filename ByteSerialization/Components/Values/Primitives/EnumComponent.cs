// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace ByteSerialization.Components.Values.Primitives
{
    public class EnumComponent : PrimitiveComponent
    {
        private Type UnderlyingType { get; set; }
        
        protected override void GetFuncs()
        {
            if (Node.Type == null)
                return;

            UnderlyingType = Enum.GetUnderlyingType(Node.Type);

            if (Reader != null)
                Read = () => Enum.ToObject(Node.Type, Reader.GetFunc(UnderlyingType)());
            if (Writer != null)
                Write = obj => Writer.GetFunc(UnderlyingType)(Convert.ChangeType(obj, UnderlyingType));

            //Node.Size = Marshal.SizeOf(UnderlyingType);
        }
    }
}
