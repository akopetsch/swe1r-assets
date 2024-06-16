// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using System.Collections;
using System.IO;

namespace SWE1R.Assets.Blocks
{
    public interface IBlock : IEnumerable
    {
        #region Properties

        Endianness Endianness { get; set; }
        byte[] Bytes { get; }
        int Count { get; }
        byte[] Hash { get; }
        string HashString { get; }
        int PartsCount { get; }
        BlockItemType BlockItemType { get; }

        #endregion

        #region Methods

        void Load(string filename);
        void Load(Stream s);

        void Save(string filename);
        void Save();

        int IndexOf(BlockItem item);

        #endregion
    }
}
