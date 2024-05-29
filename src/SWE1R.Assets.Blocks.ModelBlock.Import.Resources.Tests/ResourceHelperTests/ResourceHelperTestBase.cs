// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Utils;

namespace SWE1R.Assets.Blocks.ModelBlock.Import.Tests.ResourcesTests
{
    public abstract class ResourceHelperTestBase<TResourceHelper>
        where TResourceHelper : ResourceHelperBase, new()
    {
        #region Properties

        protected TResourceHelper ResourceHelper { get; }

        #endregion

        #region Constructor

        public ResourceHelperTestBase() =>
            ResourceHelper = new TResourceHelper();

        #endregion

        #region Methods

        protected void AssertStreamNotNull(Stream stream) =>
            Assert.NotNull(stream);

        #endregion
    }
}
