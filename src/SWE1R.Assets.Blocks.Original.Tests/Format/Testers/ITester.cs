// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers
{
    public interface ITester // TODO: is this interface necessary?
    {
        #region Properties

        public ByteSerializerGraph ByteSerializerGraph { get; }
        public AnalyticsFixture AnalyticsFixture { get; }

        public long ValuePosition { get; }

        #endregion

        #region Methods

        public void Test();

        #endregion
    }
}
