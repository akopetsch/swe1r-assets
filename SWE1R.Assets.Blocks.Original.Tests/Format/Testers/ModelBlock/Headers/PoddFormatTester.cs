// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers
{
    public class PoddFormatTester : HeaderFormatTester<PoddHeader>
    {
        public PoddFormatTester(PoddHeader header) : base(header)
        { }

        public override void Test(Graph byteSerializerGraph)
        {
            AssertHeader();
            AssertHeaderNodes();

            AssertRootNode();
            AssertMainModel();
            AssertLowPolyModel();

            Assert_02();

            Assert_17();
            Assert_17_5066(byteSerializerGraph);
            Assert_17_5066_5064(byteSerializerGraph);

            AssertAnim();
            AssertAltN(byteSerializerGraph);
        }

        private void AssertHeader()
        {
            Assert.True(Header.Nodes.Count == 75);
            Assert.True(Header.Data == null);
            Assert.True(
                Header.Animations == null ||
                Header.Animations.Count == 1 ||
                Header.Animations.Count == 16);
            Assert.True(Header.AltN.Count == 2 || Header.AltN.Count == 4);
        }

        private void AssertHeaderNodes()
        {
            // count
            var count = Header.Nodes.Where(n => n.FlaggedNode != null).Count();
            Assert.True(count >= 24 && count <= 53);

            // types
            var types = Header.Nodes
                .Where(n => n.FlaggedNode != null)
                .Select(n => n.FlaggedNode.GetType())
                .Distinct().ToList();
            Assert.True(types.Count == 3);
            Assert.True(types.Contains(typeof(Group5064)));
            Assert.True(types.Contains(typeof(Group5065)));
            Assert.True(types.Contains(typeof(TransformableD065)));

            // properties
            Assert.True(Header.Node02 != null);
            Assert.True(Header.Node10 != null);
            Assert.True(Header.Node17 != null);
            if (Header.Nodes[18].FlaggedNode != null) Assert.True(Header.Node18 != null); // TODO: strange assertion
            Assert.True(Header.Node31 != null);
            if (Header.Nodes[34].FlaggedNode != null) Assert.True(Header.Node34 != null); // TODO: strange assertion 
            Assert.True(Header.Node74 != null);
        }

        private void AssertRootNode()
        {
            Assert.True(Header.Root != null);
            Assert.True(Header.Root.Children.Count == 2);
        }

        private void AssertMainModel()
        {
            Assert.True(Header.MainModel != null);
            Assert.True(Header.MainModel.Children.Contains(Header.Node10));
        }

        private void AssertLowPolyModel()
        {
            Assert.True(Header.LowPolyModel != null);
            Assert.True(Header.LowPolyModel == Header.Node74);

            // no collision
            var meshes = Header.LowPolyModel.GetLeaves().OfType<Mesh>().ToList(); // TODO: always empty, because FlaggedNode instead of INode
            Assert.True(!meshes.Exists(m => m.CollisionVertices.ShortVectors.Count > 0));
        }

        #region Assert_02*

        private void Assert_02()
        {
            // 17 / D064
            Assert.True(Header.Node02.Children.Count == 1);
            Assert.True(Header.Node02_D064 != null);

            // 17 / D064 / (5064|D065)
            Assert.True(Header.Node02_D064.Children.Count == 1);
            Assert.True(Header.Node02_D064.Children.Are<Group5064, TransformableD065>());

            if (Header.Node02_D064_5064 != null)
            {
                int count = Header.Node02_D064_5064.Children.Count;
                Assert.True(count == 1 || count == 2 || count == 5);
            }
            // TODO: analyze: Node17 is descendant of Node02
        }

        #endregion

        #region Assert_17*

        private void Assert_17()
        {
            int count = Header.Node17.Children.Count;
            Assert.True(count == 1 || count == 3 || count == 7);
            Assert.True(Header.Node17.Children.Are<Group5066, Group5064, TransformableD065>());
        }

        private void Assert_17_5066(Graph g)
        {
            bool isAltNChild = Header.AltN
                .Select(altN => altN.Group5066ChildReference.Group5066).Contains(Header.Node17_5066);
            bool hasNullChildren = Header.Node17_5066.Children.Contains(null);

            if (Header.AltN.Count == 2)
            {
                Assert.True(!isAltNChild);
                Assert.True(!hasNullChildren);
            }
            if (Header.AltN.Count == 4)
            {
                Assert.True(isAltNChild);
                Assert.True(hasNullChildren);
            }
        }

        private void Assert_17_5066_5064(Graph g)
        {
            Assert.True(Header.Node17_5066.Children.Where(n => n != null).Are<Group5064, MeshGroup3064>());
        }

        #endregion

        private void AssertAltN(Graph g)
        {
            // count (distinct)
            var distinct = Header.AltN.Select(altN => altN.Group5066ChildReference.Group5066).Distinct().ToList();
            if (Header.AltN.Count == 2)
                Assert.True(distinct.Count == 1);
            if (Header.AltN.Count == 4)
                Assert.True(distinct.Count == 3);

            // count (except null)
            foreach (var a in distinct)
                Assert.True(a.Children.Where(n => n != null).Count() == 3);

            // count (null)
            if (Header.AltN.Count == 2)
                Assert.True(distinct[0].Children.Where(n => n == null).Count() == 2);
            if (Header.AltN.Count == 4)
            {
                Assert.True(distinct[0].Children.Where(n => n == null).Count() == 1);
                Assert.True(distinct[1].Children.Where(n => n == null).Count() == 1);
                Assert.True(distinct[2].Children.Where(n => n == null).Count() == 2);
            }
        }

        private void AssertAnim()
        {
            Assert.True(
                Header.Animations == null ||
                Header.Animations.Count == 1 ||
                Header.Animations.Count == 16);
        }
    }
}
