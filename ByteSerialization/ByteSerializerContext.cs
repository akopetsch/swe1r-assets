// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO.Extensions;
using ByteSerialization.Nodes;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Mode = ByteSerialization.ByteSerializerMode;
using Reader = ByteSerialization.IO.EndianBinaryReader;
using Writer = ByteSerialization.IO.EndianBinaryWriter;

namespace ByteSerialization
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class ByteSerializerContext
    {
        private string DebuggerDisplay =>
            $"0x{Position.ToHexString()} | {Mode}";

        #region Properties

        public Stream Stream { get; }
        public Reader Reader { get; }
        public Writer Writer { get; }
        public Mode Mode { get; }
        public long Position
        {
            get => Stream.Position;
            set => Stream.Position = value;
        }
        public Graph Graph { get; }
        public StringBuilder Log { get; }

        #endregion

        #region Constructor

        public ByteSerializerContext(Reader reader) : this(reader.BaseStream, reader, null, Mode.Deserializing) { }
        public ByteSerializerContext(Writer writer) : this(writer.BaseStream, null, writer, Mode.Serializing) { }
        private ByteSerializerContext(Stream stream, Reader reader, Writer writer, Mode mode)
        {
            Stream = stream;
            Reader = reader;
            Writer = writer;
            Mode = mode;
            Graph = new Graph();
            Log = new StringBuilder();
        }

        #endregion

        #region Methods

        public void ConsumeBytes(long n)
        {
            switch (Mode)
            {
                case Mode.Serializing: Writer.Write(new byte[n]); break;
                case Mode.Deserializing: Reader.ReadBytes(n); break;
            }
        }

        public void EnsureOffsetFrom(int offset, Node node)
        {
            long actual = Position;
            long target = node.Position.Value + offset;

            if (actual < target)
            {
                ConsumeBytes(target - actual);
            }
            else if (actual > target)
            {
                if (Mode == Mode.Deserializing)
                    // jump back to target
                    Position = target;
                else
                    // not supported
                    throw new InvalidOperationException();
            }
        }

        public void EnsureAlignment(int alignment)
        {
            if (alignment != 0)
            {
                long actual = Position;
                long target = Position.Ceiling(alignment);
                ConsumeBytes(target - actual);
            }
        }

        #endregion
    }
}
