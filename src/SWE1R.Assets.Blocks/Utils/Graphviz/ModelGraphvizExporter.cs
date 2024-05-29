// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Values.Composites.Records;
using ByteSerialization.IO.Extensions;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ByteSerializationGraph = ByteSerialization.Nodes.ByteSerializerGraph;

namespace SWE1R.Assets.Blocks.Utils.Graphviz
{
    public class ModelGraphvizExporter // TODO: FIXME: refactor, clean-up ModelGraphvizExporter
    {
        #region Fields

        protected static readonly string tab = new string(' ', 4);

        protected string _modelNodeName;
        protected Dictionary<INode, string> _nodeNodeNamesByNode = 
            new Dictionary<INode, string>();
        // TODO: FIXME: use classes to pair/encapsulate data

        #endregion

        #region Properties

        public ModelBlockItem ModelBlockItem { get; }
        public ByteSerializationGraph ByteSerializationGraph { get; }
        public string Suffix { get; }

        public string ModelName { get; }
        public StringBuilder DotFile { get; }
        public string DotFilename { get; }
        public string SvgFilename { get; }

        #endregion

        #region Constructor

        public ModelGraphvizExporter(
            ModelBlockItem modelBlockItem, ByteSerializationGraph byteSerializationGraph, string suffix)
        {
            ModelBlockItem = modelBlockItem;
            ByteSerializationGraph = byteSerializationGraph;
            Suffix = suffix;

            ModelName = new MetadataProvider().GetBlockItemValueByHash(modelBlockItem)?.Name;

            DotFile = new StringBuilder();
            DirectoryInfo dotDirectory = Directory.CreateDirectory("dot");
            DotFilename = Path.Combine(dotDirectory.Name, $"{ModelBlockItem.Index:000}.{Suffix}.dot");
            SvgFilename = Path.Combine(dotDirectory.Name, $"{ModelBlockItem.Index:000}.{Suffix}.svg");
        }

        #endregion

        #region Methods (export)

        public void Export()
        {
            DotFile.AppendLine($"digraph {ModelName} {{");
            WriteDigraph();
            DotFile.AppendLine("}");

            File.WriteAllText(DotFilename, DotFile.ToString());
            DotToSvg(DotFilename, SvgFilename);

            // TODO: FIXME: test OpenFile
            //OpenFile(SvgPath);
        }

        protected virtual void WriteDigraph()
        {
            Model model = ModelBlockItem.Model;

            // get INode RecordComponents
            var nodeRecordComponents = ByteSerializationGraph.GetRecordComponents<INode>()
                .OrderBy(rc => rc.Position).ToList();

            // get names by INode
            foreach (RecordComponent nodeRecordComponent in nodeRecordComponents)
            {
                // key: node
                var node = nodeRecordComponent.Value as INode;

                // value: name
                string position = nodeRecordComponent.Position.Value.ToHexString();
                string prefix = (node as FlaggedNode)?.Flags.ToHexString() ?? nameof(Mesh);
                string name = $"\"{prefix}_{position}\"";
                
                _nodeNodeNamesByNode.Add(node, name);
            }

            var firstFlaggedNode = ByteSerializationGraph.GetRecordComponents<FlaggedNode>()
                .OrderBy(vn => vn.Position).First().Value as FlaggedNode;

            DotFile.AppendLine($"{tab}rankdir=LR;");
            DotFile.AppendLine($"{tab}label=\"{ModelName}\";");

            // vertices
            DotFile.AppendLine($"{tab}");
            DotFile.AppendLine($"{tab}// vertices");

            #region INode

            foreach (RecordComponent nodeRecordComponent in nodeRecordComponents)
            {
                var node = (INode)nodeRecordComponent.Value;

                var attributes = new Dictionary<string, string>();

                // style
                attributes.Add("style", "filled");

                // fillcolor
                string color = null;
                var flaggedNode = node as FlaggedNode;
                if (flaggedNode != null && model.HasExtraAlignment(flaggedNode, ByteSerializationGraph))
                    color = "green";
                color = color ?? GetNodeColor(node.GetType());
                attributes.Add("fillcolor", color);

                // shape
                if (node == firstFlaggedNode)
                    attributes.Add("shape", "tripleoctagon");
                else if (node is Mesh)
                    attributes.Add("shape", "septagon");

                string attributesString = string.Join(",", attributes.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                DotFile.AppendLine($"{tab}{_nodeNodeNamesByNode[node]}[{attributesString}];");
            }

            #endregion

            // edges
            DotFile.AppendLine($"{tab}");
            DotFile.AppendLine($"{tab}// edges");

            //var nodes = header.Nodes.Select(x => x.FlaggedNode).Where(n => n != null).ToList();
            //var altNs = header.AltN?.Select(x => x.FlaggedNode).Where(n => n != null).ToList() ?? new List<FlaggedNode>();
            //var allKnown = nodes.Concat(altNs).ToList();
            //var toWrite = allKnown.Concat(allKnown.SelectMany(x => x.GetDescendants())).Distinct().ToList();
            var toWrite = nodeRecordComponents.Select(rc => (INode)rc.Value).OfType<FlaggedNode>().ToList();
            foreach (FlaggedNode flaggedNode in toWrite)
            {
                List<INode> childNodes = flaggedNode.Children?.Where(c => c != null).ToList();
                for (int i = 0; i < childNodes?.Count; i++)
                    DotFile.AppendLine($"{tab}{_nodeNodeNamesByNode[flaggedNode]} -> {_nodeNodeNamesByNode[childNodes[i]]} [label={i}];");
            }

            // Header.Nodes
            _modelNodeName = model.Type.ToString();
            DotFile.AppendLine($"{tab}{_modelNodeName}[style=filled,fillcolor=red]");

            DotFile.AppendLine($"{tab}Nodes[style=filled,fillcolor=pink];");
            DotFile.AppendLine($"{tab}{_modelNodeName} -> Nodes;");
            for (int i = 0; i < model.Nodes.Count; i++)
            {
                FlaggedNode flaggedNode = model.Nodes[i].FlaggedNode;
                if (flaggedNode != null)
                    DotFile.AppendLine($"{tab}Nodes -> \"[{i}]\" -> {_nodeNodeNamesByNode[flaggedNode]};");
            }

            // Header.AltN
            if (model.AltN != null)
            {
                DotFile.AppendLine($"{tab}AltN[style=filled,fillcolor=cyan];");
                DotFile.AppendLine($"{tab}{_modelNodeName} -> {nameof(Model.AltN)}");

                for (int i = 0; i < model.AltN.Count; i++)
                {
                    FlaggedNodeOrGroup5066ChildReference alt = model.AltN[i];
                    FlaggedNode flaggedNode = alt.Group5066ChildReference?.Group5066 ?? alt.FlaggedNode;
                    DotFile.AppendLine($"{tab}{nameof(Model.AltN)} -> \"{nameof(Model.AltN)}[{i}]\" -> {_nodeNodeNamesByNode[flaggedNode]};");
                }
            }
        }

        private string GetNodeColor(Type type)
        {
            var colorByType = new Dictionary<Type, string>()
            {
                { typeof(Mesh), "dodgerblue" },
                { typeof(MeshGroup3064), "lightblue" },

                { typeof(Group5064), "greenyellow" },
                { typeof(Group5065), "lime" },
                { typeof(Group5066), "limegreen" },

                { typeof(TransformableD064), "yellow" },
                { typeof(TransformableD065), "gold" },
                { typeof(UnknownD066), "goldenrod" },
            };
            // http://graphviz.org/doc/info/colors.html
            // http://www.webgraphviz.com/

            return colorByType[type];
        }

        #endregion

        #region Methods (dot to svg)

        private void DotToSvg(string dot, string svg) =>
            ExecuteDot(
                $"-Tsvg \"{dot.Replace(Path.DirectorySeparatorChar, '/')}\" " +
                $"-o \"{svg.Replace(Path.DirectorySeparatorChar, '/')}\"");

        private void ExecuteDot(string arguments, string directory = null)
        {
            var p = new Process();
            p.StartInfo.FileName = "dot";
            p.StartInfo.Arguments = arguments;
            p.StartInfo.WorkingDirectory = directory ?? Directory.GetCurrentDirectory();
            p.Start();
            p.WaitForExit();
        }

        #endregion

        #region Methods (open file)

        public void OpenDotFile() => 
            OpenFile(DotFilename);

        public void OpenSvgFile() =>
            OpenFile(SvgFilename);

        private void OpenFile(string filename)
        {
            string uri = "file:///" + ReplaceDirectorySeparatorChar(Path.GetFullPath(filename), '/');
            var si = new ProcessStartInfo(uri);
            si.UseShellExecute = true;
            Process.Start(si);
        }

        private string ReplaceDirectorySeparatorChar(string path, char newChar) =>
            string.Join($"{newChar}", path.Split(Path.DirectorySeparatorChar));

        #endregion
    }
}
