// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models;
using System.Collections;
using System.IO;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using Swe1rModelBlockItem = SWE1R.Assets.Blocks.ModelBlock.ModelBlockItem;
using Swe1rSplineBlockItem = SWE1R.Assets.Blocks.SplineBlock.SplineBlockItem;
using Swe1rSpriteBlockItem = SWE1R.Assets.Blocks.SpriteBlock.SpriteBlockItem;
using Swe1rTextureBlockItem = SWE1R.Assets.Blocks.TextureBlock.TextureBlockItem;

namespace SWE1R.Assets.Blocks.Unity.Editor
{
    public class BlockEditorWindow : EditorWindow
    {
        #region Constants

        private const string gameName = "Star Wars Episode I: Racer";
        private const string editorWindowName = "Block Editor";
        private static readonly string titleString = $"{gameName} {editorWindowName}";

        #endregion

        #region Fields

        private string blocksPath = @"C:/apps/swe1r/gog-hotfix3/data/lev01"; // TODO: configurable blocksPath

        private string modelBlockFilename = BlockDefaultFilenames.ModelBlock;
        private string textureBlockFilename = BlockDefaultFilenames.TextureBlock;
        private string splineBlockFilename = BlockDefaultFilenames.SplineBlock;
        private string spriteBlockFilename = BlockDefaultFilenames.SpriteBlock;

        private int modelIndex = 0;

        private Block<Swe1rModelBlockItem> modelBlock;
        private Block<Swe1rTextureBlockItem> textureBlock;
        private Block<Swe1rSplineBlockItem> splineBlock;
        private Block<Swe1rSpriteBlockItem> spriteBlock;

        private EditorCoroutine importAllCoroutine;

        #endregion

        #region Methods (gui)

        [MenuItem("Window/" + gameName + "/" + editorWindowName)]
        public static void Init() =>
            GetWindow<BlockEditorWindow>().titleContent = new GUIContent(titleString);

        public void OnGUI()
        {
            EditorGUILayout.Space();
            GuiDeleteAllAssets();
            EditorGUILayout.Space();
            GuiBlocksPath();
            EditorGUILayout.Space();
            GuiModelBlock();
            EditorGUILayout.Space();
            GuiTextureBlock();
            EditorGUILayout.Space();
            GuiSplineBlock();
            EditorGUILayout.Space();
            GuiSpriteBlock();
            EditorGUILayout.Space();
        }

        private void GuiDeleteAllAssets()
        {
            if (GuiButton("Delete All Assets"))
                AssetsHelper.DeleteAllAssets();
        }

        private void GuiBlocksPath()
        {
            const string label = "Blocks Path";
            EditorGUILayout.BeginHorizontal();
            blocksPath = EditorGUILayout.TextField(label, blocksPath);
            if (GuiButton("..."))
            {
                string newBlocksPath = EditorUtility.OpenFolderPanel(label, blocksPath, "");
                if (!string.IsNullOrEmpty(newBlocksPath))
                    blocksPath = newBlocksPath;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void GuiModelBlock()
        {
            EditorGUILayout.LabelField("Model Block");

            // title
            modelBlockFilename = GuiFilenameField(modelBlockFilename);

            // index int field, import/export buttons
            EditorGUILayout.BeginHorizontal();
            modelIndex = GuiIntField("Index", modelIndex, 3);
            if (GuiButton("Import"))
                EditorCoroutineUtility.StartCoroutine(Import(modelIndex), this);
            if (GuiButton("Export"))
                EditorCoroutineUtility.StartCoroutine(Export(modelIndex), this);
            if (GuiButton("Export (Block)"))
                EditorCoroutineUtility.StartCoroutine(ExportToBlock(modelIndex), this);
            if (GuiButton("Test Re-Export"))
                EditorCoroutineUtility.StartCoroutine(TestReExport(modelIndex), this);

            EditorGUILayout.EndHorizontal();

            // import all
            EditorGUILayout.LabelField("Test Re-Export All");
            EditorGUILayout.BeginHorizontal();
            if (importAllCoroutine == null)
            {
                if (GuiButton("Start"))
                    importAllCoroutine = EditorCoroutineUtility.StartCoroutine(TestReExportAll(), this);
            }
            else if (importAllCoroutine != null)
            {
                if (GuiButton("Stop"))
                {
                    EditorCoroutineUtility.StopCoroutine(importAllCoroutine);
                    importAllCoroutine = null;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void GuiTextureBlock()
        {
            EditorGUILayout.LabelField("Texture Block");
            textureBlockFilename = GuiFilenameField(textureBlockFilename);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
        }

        private void GuiSplineBlock()
        {
            EditorGUILayout.LabelField("Spline Block");
            splineBlockFilename = GuiFilenameField(splineBlockFilename);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
        }

        private void GuiSpriteBlock()
        {
            EditorGUILayout.LabelField("Sprite Block");
            spriteBlockFilename = GuiFilenameField(splineBlockFilename);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region Methods (GUI helper)

        private int GuiIntField(string label, int value, int digits)
        {
            // return EditorGUILayout.IntField(label, value);

            GUIStyle style = new GUIStyle(EditorStyles.numberField);
            style.stretchWidth = false;
            return EditorGUILayout.IntField(label, value, style);
        }

        private string GuiFilenameField(string filename) =>
            EditorGUILayout.TextField("Filename", filename);

        private bool GuiButton(string s)
        {
            // return GUILayout.Button(s);

            GUIContent content = new GUIContent(s);

            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.stretchWidth = false;

            Vector2 size = style.CalcSize(content);

            return GUI.Button(GUILayoutUtility.GetRect(size.x, size.y, style), content, style);
        }

        #endregion

        // TODO: use synchronous nested coroutines to re-use code
        // https://www.alanzucconi.com/2017/02/15/nested-coroutines-in-unity/

        #region Methods (import)

        private IEnumerator Import(int modelIndex)
        {
            ModelImporter importer = GetModelImporter(modelIndex);
            int i = importer.ModelIndex;

            LogImport(i); yield return null;
            ImportAndSelect(importer);
            LogImported(i); yield return null;
        }

        private ModelImporter GetModelImporter(int modelIndex)
        {
            LoadBlocks();
            return new ModelImporter(modelBlock, modelIndex, textureBlock);
        }

        private void ImportAndSelect(ModelImporter importer)
        {
            importer.Import();
            Selection.activeGameObject = importer.GameObject;
        }

        #endregion

        #region Methods (export)

        private IEnumerator Export(int modelIndex)
        {
            ModelExporter exporter = GetModelExporter(modelIndex);
            
            LogExport(modelIndex); yield return null;
            exporter.Export();
            LogExported(modelIndex); yield return null;
        }

        private IEnumerator ExportToBlock(int modelIndex)
        {
            ModelExporter exporter = GetModelExporter(modelIndex);
            if (exporter != null)
            {
                LogExport(modelIndex); yield return null;
                exporter.Export();
                LogExported(modelIndex); yield return null;

                if (exporter.ModelBlockItem != null)
                {
                    LogExportToModelBlock(modelIndex); yield return null;
                    LoadBlocks(); // TODO: only load modelblock
                    modelBlock[modelIndex] = exporter.ModelBlockItem;
                    SaveBlocks(); // TODO: only save modelblock
                    LogExportedToModelBlock(modelIndex); yield return null;
                }
            }
        }

        private ModelExporter GetModelExporter(int modelIndex)
        {
            ModelComponent modelComponent = GetSelectedGameObjectModelComponent();
            if (modelComponent == null)
                return null;
            else
                return new ModelExporter(modelComponent, modelIndex);
        }

        private ModelComponent GetSelectedGameObjectModelComponent()
        {
            var gameObject = Selection.objects.SingleOrDefault() as GameObject;
            if (gameObject == null)
            {
                Debug.LogError("Selection is not a single GameObject to export.");
                return null;
            }

            var modelComponent = gameObject.GetComponent<ModelComponent>();
            if (modelComponent == null)
            {
                Debug.LogError("Selection is not an imported SWE1R model.");
                return null;
            }
            return modelComponent;
        }

        #endregion

        #region Methods (re-export)

        private IEnumerator TestReExport(int modelIndex)
        {
            ModelImporter importer = GetModelImporter(modelIndex);
            
            LogTestReExport(modelIndex); yield return null;

            LogImport(modelIndex); yield return null;
            ImportAndSelect(importer);
            LogImported(modelIndex); yield return null;

            ModelExporter exporter = GetModelExporter(modelIndex);
            bool successful = false;
            if (exporter != null)
            {
                LogExport(modelIndex); yield return null;
                exporter.Export();
                LogExported(modelIndex); yield return null;

                if (exporter.ModelBlockItem != null)
                {
                    successful = Enumerable.SequenceEqual(importer.ModelBlockItem.Bytes, exporter.ModelBlockItem.Bytes);
                    if (successful)
                    {
                        importer.AssetsHelper.DeleteAssets();
                        DestroyImmediate(Selection.objects.SingleOrDefault() as GameObject);
                        LogTestReExportSuccessful(modelIndex); yield return null;
                    }
                }
            }
            if (!successful)
            {
                LogTestReExportFailed(modelIndex);
                yield return null;
            }
        }

        private IEnumerator TestReExportAll()
        {
            LogTestReExportAll(); yield return null;

            LoadBlocks();
            for (int i = 0; i < modelBlock.Count; i++)
                yield return EditorCoroutineUtility.StartCoroutine(TestReExport(i), this);

            LogTestedReExportAllFinished(); yield return null;

            importAllCoroutine = null;
        }

        #endregion

        #region Methods (log)

        private void LogImport(int modelIndex) =>
            Debug.Log($"Import model {modelIndex}...");

        private void LogImported(int modelIndex) =>
            Debug.Log($"Imported model {modelIndex}.");

        private void LogExport(int modelIndex) =>
            Debug.Log($"Export model {modelIndex}...");

        private void LogExported(int modelIndex) =>
            Debug.Log($"Exported model {modelIndex}.");

        private void LogExportToModelBlock(int modelIndex) =>
            Debug.Log($"Export model {modelIndex} to block...");

        private void LogExportedToModelBlock(int modelIndex) =>
            Debug.Log($"Exported model {modelIndex} to block.");

        private void LogTestReExport(int modelIndex) =>
            Debug.Log($"Test re-export of model {modelIndex}...");

        private void LogTestReExportSuccessful(int modelIndex) =>
            Debug.Log($"Test re-export of model {modelIndex} successful.");

        private void LogTestReExportFailed(int modelIndex) =>
            Debug.LogError($"Test re-export of model {modelIndex} failed.");

        private void LogTestReExportAll() =>
            Debug.Log($"Test re-export all.");

        private void LogTestedReExportAllFinished() =>
            Debug.Log($"Test re-export all finished.");

        #endregion

        #region Methods (blocks helper)

        private void LoadBlocks()
        {
            modelBlock = LoadBlock<Swe1rModelBlockItem>(modelBlockFilename);
            textureBlock = LoadBlock<Swe1rTextureBlockItem>(textureBlockFilename);
            splineBlock = LoadBlock<Swe1rSplineBlockItem>(splineBlockFilename);
            spriteBlock = LoadBlock<Swe1rSpriteBlockItem>(spriteBlockFilename);
        }

        private Block<TBlockItem> LoadBlock<TBlockItem>(string filename) where TBlockItem : BlockItem, new()
        {
            var block = new Block<TBlockItem>();
            block.Load(Path.Combine(blocksPath, filename));
            return block;
        }

        private void SaveBlocks()
        {
            if (!Directory.Exists(blocksPath))
                Debug.LogError($"Directory \"{blocksPath}\" does not exist.");

            modelBlock?.Save(Path.Combine(blocksPath, modelBlockFilename));
            textureBlock?.Save(Path.Combine(blocksPath, textureBlockFilename));
            splineBlock?.Save(Path.Combine(blocksPath, splineBlockFilename));
            spriteBlock?.Save(Path.Combine(blocksPath, spriteBlockFilename));
        }

        #endregion
    }
}
