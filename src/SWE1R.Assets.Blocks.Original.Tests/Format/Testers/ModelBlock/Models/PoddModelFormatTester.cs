// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Extensions;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models
{
    public class PoddModelFormatTester : ModelFormatTester<PoddModel>
    {
        public override void Test()
        {
            base.Test();

            AssertHeader();
            AssertHeaderNodes();

            AssertRootNode();
            AssertMainModel();
            AssertLowPolyModel();

            Assert_02();

            Assert_17();
            Assert_17_5066();
            Assert_17_5066_5064();

            AssertAnimations();
            AssertAltN();
        }

        private void AssertHeader()
        {
            Assert.True(Value.Nodes.Count == 75);
            Assert.True(Value.Data == null);
            Assert.True(
                Value.Animations == null ||
                Value.Animations.Count == 1 ||
                Value.Animations.Count == 16);
            Assert.True(Value.AltN.Count == 2 || Value.AltN.Count == 4);
        }

        private void AssertHeaderNodes()
        {
            // count
            var count = Value.Nodes.Where(n => n.FlaggedNode != null).Count();
            Assert.True(count >= 24 && count <= 53);

            // types
            var types = Value.Nodes
                .Where(n => n.FlaggedNode != null)
                .Select(n => n.FlaggedNode.GetType())
                .Distinct().ToList();
            Assert.True(types.Count == 3);
            Assert.Contains(typeof(Group5064), types);
            Assert.Contains(typeof(Group5065), types);
            Assert.Contains(typeof(TransformableD065), types);

            // properties
            Assert.True(Value.Node02 != null);
            Assert.True(Value.Node10 != null);
            Assert.True(Value.Node17 != null);
            if (Value.Nodes[18].FlaggedNode != null) Assert.True(Value.Node18 != null); // TODO: strange assertion
            Assert.True(Value.Node31 != null);
            if (Value.Nodes[34].FlaggedNode != null) Assert.True(Value.Node34 != null); // TODO: strange assertion 
            Assert.True(Value.Node74 != null);
        }

        private void AssertRootNode()
        {
            Assert.True(Value.Root != null);
            Assert.True(Value.Root.Children.Count == 2);
        }

        private void AssertMainModel()
        {
            Assert.True(Value.MainModel != null);
            Assert.Contains(Value.Node10, Value.MainModel.Children);
        }

        private void AssertLowPolyModel()
        {
            Assert.True(Value.LowPolyModel != null);
            Assert.True(Value.LowPolyModel == Value.Node74);

            // no collision
            var meshes = Value.LowPolyModel.GetLeaves().OfType<Mesh>().ToList();
            Assert.True(!meshes.Exists(m => m.CollisionVertices.ShortVectors.Count > 0));
        }

        #region Assert_02*

        private void Assert_02()
        {
            // 17 / D064
            Assert.True(Value.Node02.Children.Count == 1);
            Assert.True(Value.Node02_D064 != null);

            // 17 / D064 / (5064|D065)
            Assert.True(Value.Node02_D064.Children.Count == 1);
            Assert.True(Value.Node02_D064.Children.Are<Group5064, TransformableD065>());

            if (Value.Node02_D064_5064 != null)
            {
                int count = Value.Node02_D064_5064.Children.Count;
                Assert.True(count == 1 || count == 2 || count == 5);
            }
            // TODO: analyze: Node17 is descendant of Node02
        }

        #endregion

        #region Assert_17*

        private void Assert_17()
        {
            int count = Value.Node17.Children.Count;
            Assert.True(count == 1 || count == 3 || count == 7);
            Assert.True(Value.Node17.Children.Are<Group5066, Group5064, TransformableD065>());
        }

        private void Assert_17_5066()
        {
            bool isAltNChild = Value.AltN
                .Select(altN => altN.Group5066ChildReference.Group5066).Contains(Value.Node17_5066);
            bool hasNullChildren = Value.Node17_5066.Children.Contains(null);

            if (Value.AltN.Count == 2)
            {
                Assert.True(!isAltNChild);
                Assert.True(!hasNullChildren);
            }
            if (Value.AltN.Count == 4)
            {
                Assert.True(isAltNChild);
                Assert.True(hasNullChildren);
            }
        }

        private void Assert_17_5066_5064()
        {
            Assert.True(Value.Node17_5066.Children.Where(n => n != null).Are<Group5064, MeshGroup3064>());
        }

        #endregion

        private void AssertAltN()
        {
            // count (distinct)
            var distinct = Value.AltN.Select(altN => altN.Group5066ChildReference.Group5066).Distinct().ToList();
            if (Value.AltN.Count == 2)
                Assert.True(distinct.Count == 1);
            if (Value.AltN.Count == 4)
                Assert.True(distinct.Count == 3);

            // count (except null)
            foreach (var a in distinct)
                Assert.True(a.Children.Where(n => n != null).Count() == 3);

            // count (null)
            if (Value.AltN.Count == 2)
                Assert.True(distinct[0].Children.Where(n => n == null).Count() == 2);
            if (Value.AltN.Count == 4)
            {
                Assert.True(distinct[0].Children.Where(n => n == null).Count() == 1);
                Assert.True(distinct[1].Children.Where(n => n == null).Count() == 1);
                Assert.True(distinct[2].Children.Where(n => n == null).Count() == 2);
            }
        }

        private void AssertAnimations()
        {
            Assert.True(
                Value.Animations == null ||
                Value.Animations.Count == 1 ||
                Value.Animations.Count == 16);
        }
    }
}
