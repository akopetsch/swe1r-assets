// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity
{
    public class AssetsHelper
    {
        #region Fields

        private static string rootPath { get; } =
            Path.Combine("Assets", "SWE1R");

        #endregion

        #region Properties

        public string Name { get; }

        #endregion

        #region Constructor

        public AssetsHelper(string name) =>
            Name = name;

        #endregion

        #region Methods

        public void DeleteAssets()
        {
            if (AssetDatabase.DeleteAsset(Path.Combine(rootPath, Name)))
                Debug.Log($"Deleted all SWE1R assets of \"{Name}\".");
            else
                Debug.LogError($"Failed to delete all SWE1R assets of \"{Name}\".");
        }

        public static void DeleteAllAssets()
        {
            string[] folders = AssetDatabase.GetSubFolders(rootPath);
            var outFailedPaths = new List<string>();
            if (AssetDatabase.DeleteAssets(folders, outFailedPaths))
                Debug.Log("Deleted all SWE1R assets.");
            else
            {
                string newLine = Environment.NewLine;
                string pathsString = string.Join(newLine, outFailedPaths);
                Debug.LogError($"Failed to delete all SWE1R assets.{newLine}{newLine}{pathsString}{newLine}");
            }
        }

        private static void LogDeletionError(string message, List<string> outFailedPaths)
        {
            string newLine = Environment.NewLine;
            string pathsString = string.Join(newLine, outFailedPaths);
            Debug.LogError($"{message}{newLine}{newLine}{pathsString}{newLine}");
        }

        public static GameObject GetAssetGameObject(GameObject gameObject) // TODO: unused
        {
            // return PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
            // differant than PrefabUtility.GetCorrespondingObjectFromSource
            var assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            GameObject assetGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            return assetGameObject;
        }

        public void SaveAsAsset(ScriptableObject scriptableObject, string subfolderName, string assetName)
        {
            // https://discussions.unity.com/t/saving-a-scriptable-object-to-the-assets-folder-and-creating-a-folder-for-it-to-go-into/239300

            string folderPath = Path.Combine(rootPath, Name, subfolderName);
            CreateAssetsFolder(folderPath);
            string fileName = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(folderPath, assetName));

            AssetDatabase.CreateAsset(scriptableObject, fileName);
        }

        public GameObject SaveAsPrefabAssetAndConnect(FlaggedNodeComponent flaggedNodeComponent)
        {
            GameObject gameObject = flaggedNodeComponent.gameObject;

            string folderPath = Path.Combine(rootPath, Name, nameof(FlaggedNode));
            CreateAssetsFolder(folderPath);
            string filename = AssetDatabase.GenerateUniqueAssetPath(
                Path.Combine(folderPath, $"{gameObject.name}.prefab"));

            // save prefab
            bool prefabSuccess;
            GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(
                gameObject, filename, InteractionMode.AutomatedAction, out prefabSuccess);
            if (!prefabSuccess)
                Debug.LogError($"Failed to save prefab \"{filename}\"");

            return prefab; // TODO: returns prefab asset or instance?
        }

        private void CreateAssetsFolder(string path)
        {
            string[] segments = path.Split(Path.DirectorySeparatorChar);

            for (int i = 0; i < segments.Length; i++)
            {
                string subPath = Path.Combine(segments.Take(i + 1).ToArray());
                if (!Directory.Exists(subPath))
                {
                    string parentFolder = Path.Combine(segments.Take(i).ToArray());
                    string newFolderName = segments[i];
                    AssetDatabase.CreateFolder(parentFolder, newFolderName);
                }
            }
        }

        #endregion
    }
}
