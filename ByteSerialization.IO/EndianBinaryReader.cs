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
    // TODO: make this class more like BinaryReader

    public class EndianBinaryReader : IDisposable
    {
        #region Fields

        private BinaryReader reader;
        private Dictionary<Type, ReadFunc> funcs =
            new Dictionary<Type, ReadFunc>();

        #endregion

        #region Properties

        public Stream BaseStream { get; set; }
        public Endianness Endianness { get; set; } // FIXME: not evaluated
        public ulong Count { get; private set; } = 0;

        #endregion

        #region Constructor / Dispose

        public EndianBinaryReader(Stream stream, Endianness endianness)
        {
            BaseStream = stream;
            Endianness = endianness;

            reader = new BinaryReader(stream);
            InitFuncs();
        }

        private void InitFuncs()
        {
            funcs.Add(typeof(bool), () => ReadBoolean());
            funcs.Add(typeof(byte), () => ReadByte());
            funcs.Add(typeof(sbyte), () => ReadSByte());
            funcs.Add(typeof(short), () => ReadInt16());
            funcs.Add(typeof(ushort), () => ReadUInt16());
            funcs.Add(typeof(int), () => ReadInt32());
            funcs.Add(typeof(uint), () => ReadUInt32());
            funcs.Add(typeof(long), () => ReadInt64());
            funcs.Add(typeof(ulong), () => ReadUInt64());
            funcs.Add(typeof(float), () => ReadSingle());
            funcs.Add(typeof(double), () => ReadDouble());
            funcs.Add(typeof(decimal), () => ReadDecimal());
            funcs.Add(typeof(char), () => ReadChar());
            funcs.Add(typeof(string), () => ReadString());
        }

        public void Dispose() => reader.Close();

        #endregion

        #region Methods

        public bool ReadBoolean() => reader.ReadBoolean();
        public byte ReadByte() => reader.ReadByte();
        public sbyte ReadSByte() => reader.ReadSByte();
        public char ReadChar() => reader.ReadChar();
        public short ReadInt16() => reader.ReadInt16().SwapBytes();
        public ushort ReadUInt16() => reader.ReadUInt16().SwapBytes();
        public int ReadInt32() => reader.ReadInt32().SwapBytes();
        public uint ReadUInt32() => reader.ReadUInt32().SwapBytes();
        public long ReadInt64() => reader.ReadInt64().SwapBytes();
        public ulong ReadUInt64() => reader.ReadUInt64().SwapBytes();
        public float ReadSingle() => BitConverter.ToSingle(ReadBytes(sizeof(float)).Reverse().ToArray(), 0);
        public double ReadDouble() => BitConverter.ToDouble(ReadBytes(sizeof(double)).Reverse().ToArray(), 0);
        public decimal ReadDecimal() => ReadBytes(sizeof(decimal)).Reverse().ToArray().ToDecimal(0);
        public byte[] ReadBytes(int count) => reader.ReadBytes(count);
        public byte[] ReadBytes(long count) => reader.ReadBytes((int)count);
        public char[] ReadChars(int count) => reader.ReadChars(count);
        public string ReadString() => throw new NotImplementedException();

        public int[] ReadInt32(int count) => Read(ReadInt32, count);

        private T[] Read<T>(Func<T> func, int count)
        {
            var array = new T[count];
            for (int i = 0; i < count; i++)
                array[i] = func.Invoke();
            return array;
        }

        public ReadFunc GetFunc(Type t) => funcs[t];
        public object Read(Type t) => funcs[t].Invoke();
        public T Read<T>() => (T)Read(typeof(T));

        public T Peek<T>() => (T)Peek(typeof(T));
        public object Peek(Type t) => AtPosition(BaseStream.Position, r => r.Read(t));
        public byte[] PeekBytes(int count) => AtPosition(BaseStream.Position, r => r.ReadBytes(count));
        public char[] PeekChars(int count) => AtPosition(BaseStream.Position, r => r.ReadChars(count));

        public T AtPosition<T>(long position, Func<EndianBinaryReader, T> action)
        {
            long oldPosition = BaseStream.Position;
            BaseStream.Position = position;
            T result = action.Invoke(this);
            BaseStream.Position = oldPosition;
            return result;
        }

        #endregion
    }
}
