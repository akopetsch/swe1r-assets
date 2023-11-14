// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ByteSerialization.IO
{
    // TODO: make this class more like BinaryWriter

    public class EndianBinaryWriter : IDisposable
    {
        #region Fields

        private BinaryWriter writer;
        private Dictionary<Type, WriteFunc> funcs =
            new Dictionary<Type, WriteFunc>();

        #endregion

        #region Properties

        public Stream BaseStream { get; set; }
        public Endianness Endianness { get; set; }
        public ulong Count { get; private set; } = 0;

        #endregion

        #region Constructor

        public EndianBinaryWriter(Stream stream, Endianness endianness)
        {
            BaseStream = stream;
            Endianness = endianness;

            writer = new BinaryWriter(stream);
            InitFuncs();
        }

        private void InitFuncs()
        {
            funcs.Add(typeof(bool), x => Write((bool)x));
            funcs.Add(typeof(byte), x => Write((byte)x));
            funcs.Add(typeof(sbyte), x => Write((sbyte)x));
            funcs.Add(typeof(short), x => Write((short)x));
            funcs.Add(typeof(ushort), x => Write((ushort)x));
            funcs.Add(typeof(int), x => Write((int)x));
            funcs.Add(typeof(uint), x => Write((uint)x));
            funcs.Add(typeof(long), x => Write((long)x));
            funcs.Add(typeof(ulong), x => Write((ulong)x));
            funcs.Add(typeof(float), x => Write((float)x));
            funcs.Add(typeof(double), x => Write((double)x));
            funcs.Add(typeof(decimal), x => Write((decimal)x));
            funcs.Add(typeof(char), x => Write((char)x));
            funcs.Add(typeof(string), x => Write((string)x));
        }

        public void Dispose() => writer.Close();

        #endregion

        #region Methods

        public void Write(byte value) => writer.Write(value);
        public void Write(sbyte value) => writer.Write(value);
        public void Write(bool value) => writer.Write(value);
        public void Write(short value) => writer.Write(value.SwapBytes());
        public void Write(ushort value) => writer.Write(value.SwapBytes());
        public void Write(int value) => writer.Write(value.SwapBytes());
        public void Write(uint value) => writer.Write(value.SwapBytes());
        public void Write(long value) => writer.Write(value.SwapBytes());
        public void Write(ulong value) => writer.Write(value.SwapBytes());
        public void Write(float value) => writer.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        public void Write(double value) => writer.Write(BitConverter.DoubleToInt64Bits(value).SwapBytes());
        public void Write(decimal value) => throw new NotImplementedException();
        public void Write(char value) => writer.Write(value);
        public void Write(char[] value) => writer.Write(value);
        public void Write(string value) => writer.Write(value);
        public void Write(byte[] value) => writer.Write(value);
        public void Write(object value) => funcs[value.GetType()].Invoke(value);

        public WriteFunc GetFunc(Type t) => funcs[t];
        private void WriteObject(object value) => funcs[value.GetType()].Invoke(value);

        public void WriteAndReset(object value)
        {
            AtPosition(
                BaseStream.Position,
                w => w.Write(value));
        }

        public void AtPosition(long position, Action<EndianBinaryWriter> action)
        {
            long oldPosition = BaseStream.Position;
            BaseStream.Position = position;
            action.Invoke(this);
            BaseStream.Position = oldPosition;
        }

        #endregion
    }
}
