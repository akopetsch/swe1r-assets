﻿// SPDX-License-Identifier: MIT

using ByteSerialization.Nodes;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers
{
    public abstract class Tester<TValue> : ITester
    {
        #region Properties

        public TValue Value { get; set; }
        public ByteSerializerGraph ByteSerializerGraph { get; set; }
        public AnalyticsFixture AnalyticsFixture { get; set; }

        public long ValuePosition { get; private set; }
        
        #endregion

        #region Constructor

        public virtual void Init(TValue value, ByteSerializerGraph byteSerializerGraph, AnalyticsFixture analyticsFixture)
        {
            Value = value;
            ByteSerializerGraph = byteSerializerGraph;
            AnalyticsFixture = analyticsFixture;

            ValuePosition = ByteSerializerGraph.GetValueComponent(Value).Position.Value;
        }

        #endregion

        #region Methods

        public abstract void Test();

        #endregion
    }
}
