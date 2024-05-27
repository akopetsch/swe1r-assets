// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;

namespace ByteSerialization.Components.Values.Primitives
{
    public class PrimitiveComponent : ValueComponent
    {
        protected ReadFunc Read { get; set; }
        protected WriteFunc Write { get; set; }
        
        public override void OnAdded()
        {
            base.OnAdded();
            GetFuncs();
            Node.TypeChanged += (t1, t2) => GetFuncs();
        }

        protected virtual void GetFuncs()
        {
            if (Node.Type == null)
                return;

            Read = Reader?.GetFunc(Node.Type);
            Write = Writer?.GetFunc(Node.Type);

            //Node.Size = Marshal.SizeOf(Node.Type);
        }

        public override void Serialize() =>
            Write(Node.Value);

        public override void Deserialize() => 
            Node.Value = Read();
    }
}
