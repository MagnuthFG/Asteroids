using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth
{
    public static class AssetUtility
    {
        private static Queue<ScriptableObject> s_delayed = null;

        private const string POPUP_YES    = "Yes";
        private const string POPUP_ABORT  = "Abort";
        private const string DELETE_TITLE = "Delete selected asset?";
        private const string DELETE_MSG   = "Do you want to delete this asset?";

// ASSET LOADING

        /// <summary>
        /// Loads the visual tree asset from this file directory
        /// </summary>
        public static VisualTreeAsset LoadTreeAsset(ScriptableObject asset){
            var path = GetScriptPath(asset);
                path = path.Substring(0, path.Length - 3);

            return AssetDatabase.LoadAssetAtPath
                <VisualTreeAsset>($"{path}.uxml");
        }

// ASSET HANDLING

        /// <summary>
        /// Renames this scriptable object
        /// </summary>
        public static void RenameAsset(ScriptableObject asset, string name){
            var path = GetAssetPath(asset);
            var msg  = AssetDatabase.RenameAsset(path, name);

            if (string.IsNullOrEmpty(msg)){
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

            } else Debug.LogError(msg);
        }

        /// <summary>
        /// Moves this scriptable object to the OS trashbin
        /// </summary>
        public static void TrashAsset(ScriptableObject asset){
            var path = GetAssetPath(asset);

            AssetDatabase.MoveAssetToTrash(path);
            AssetDatabase.SaveAssets();
        }

// SUB ASSET HANDLING

        /// <summary>
        /// Creates a new scriptable object sub asset
        /// </summary>
        public static T CreateSubAsset<T>(ScriptableObject parent) where T : ScriptableObject {
            var asset = ScriptableObject.CreateInstance<T>();
            asset.name = $"New {typeof(T).Name}";

            AddSubAsset(asset, parent);
            return asset;
        }

        /// <summary>
        /// Adds this scriptable object to the parent asset
        /// </summary>
        public static void AddSubAsset(ScriptableObject asset, ScriptableObject parent){
            AssetDatabase.AddObjectToAsset(asset, parent);
            
            EditorUtility.SetDirty(asset);
            EditorUtility.SetDirty(parent);

            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// Unparents this scriptable object from its parent asset
        /// </summary>
        public static void RemoveSubAsset(ScriptableObject asset){
            var dPath = GetAssetDirectory(asset);
            var fPath = $"{dPath}{asset.name}.asset";

            AssetDatabase.RemoveObjectFromAsset(asset);
            AssetDatabase.CreateAsset(asset, fPath);

            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }


        /// <summary>
        /// Renames this scriptable object sub asset
        /// </summary>
        public static void RenameSubAsset(ScriptableObject asset, string name){
            // cant rename using asset database if sub asset?
            // it renames the parent asset if I do, how ever this works?
            asset.name = name;

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// Moves this scriptable object to the OS trashbin
        /// </summary>
        public static void TrashSubAsset(ScriptableObject asset){
            AssetDatabase.RemoveObjectFromAsset(asset);
            TrashAsset(asset);
        }

        /// <summary>
        /// Moves this scriptable object to the OS trashbin
        /// <br>The removal is delayed by one editor tick</br>
        /// </summary>
        public static void DelayedTrashSubAsset(ScriptableObject asset){
            s_delayed ??= new Queue<ScriptableObject>();
            s_delayed.Enqueue(asset);

            EditorApplication.update += DelayedTrashSubAsset;
        }

        /// <summary>
        /// Finalises the move of the delayed scriptable objects to the OS trashbin
        /// </summary>
        private static void DelayedTrashSubAsset(){
            for (int i = 0; i < s_delayed.Count; i++){
                TrashSubAsset(s_delayed.Dequeue());
            }

            EditorApplication.update -= DelayedTrashSubAsset;
        }

// POPUPS

        /// <summary>
        /// Returns if the user selected to remove the asset
        /// </summary>
        public static bool RemoveAssetPopup(){
            return EditorUtility.DisplayDialog(
                DELETE_TITLE, 
                DELETE_MSG, 
                POPUP_YES, 
                POPUP_ABORT
            );
        }

// ASSET PATHS

        /// <summary>
        /// Returns the script file path for this scriptable object
        /// </summary>
        public static string GetScriptPath(ScriptableObject asset){
            var script = MonoScript.FromScriptableObject(asset);
            return AssetDatabase.GetAssetPath(script);
        }

        /// <summary>
        /// Returns the asset file path for this scriptable object
        /// </summary>
        public static string GetAssetPath(ScriptableObject asset){
            return AssetDatabase.GetAssetPath(asset);
        }

        /// <summary>
        /// Returns the script file directory for this scriptable object
        /// </summary>
        public static string GetAssetDirectory(ScriptableObject asset){
            var path = GetAssetPath(asset);

            var start = path.LastIndexOf('/');
            path = path.Substring(0, start + 1);

            return path;
        }
    }
}