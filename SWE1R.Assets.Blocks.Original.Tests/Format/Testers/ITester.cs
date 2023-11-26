// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers
{
    public interface ITester // TODO: is this interface necessary?
    {
        #region Properties

        public Graph ByteSerializerGraph { get; }
        public AnalyticsFixture AnalyticsFixture { get; }

        public long ValuePosition { get; }

        #endregion

        #region Methods

        public void Test();

        #endregion
    }
}
