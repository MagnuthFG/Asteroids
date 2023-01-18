using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Magnuth {
    public sealed partial class SegmentSettingsEditor : Editor
    {
// ASSET HANDLING

        /// <summary>
        /// Creates a new scriptable object asset and parents it to this asset
        /// </summary>
        private void Create<T>(SerializedProperty property) where T : ScriptableObject {
            if (property.objectReferenceValue != null) return;

            var asset  = CreateInstance<T>();
            asset.name = $"New {typeof(T).Name}";

            AssetDatabase.AddObjectToAsset(asset, _target);
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(asset);
            EditorUtility.SetDirty(_target);

            // has to be assigned after the asset has been created
            property.objectReferenceValue = asset;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }

        /// <summary>
        /// Deletes the scriptable object asset from its parrent asset
        /// </summary>
        private void Delete<T>(SerializedProperty property) where T : ScriptableObject {
            if (property.objectReferenceValue == null) return;

            //var path = GetFilePath((ScriptableObject)property.objectReferenceValue);
            //AssetDatabase.DeleteAsset(path);
            //EditorUtility.SetDirty(_target);

            property.objectReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }

// ASSET LOADING

        /// <summary>
        /// Loads the visual tree asset from this file directory
        /// </summary>
        private VisualTreeAsset LoadTreeAsset(){
            var path = GetFilePath(this);
                path = path.Substring(0, path.Length - 3);

            return AssetDatabase.LoadAssetAtPath
                <VisualTreeAsset>($"{path}.uxml");
        }
        
        /// <summary>
        /// Returns the scriptable object file path
        /// </summary>
        private string GetFilePath(ScriptableObject sobj){
            var script = MonoScript.FromScriptableObject(sobj); 

            var path = AssetDatabase.GetAssetPath(script);

            return new FileInfo(path).ToString();
        }
    }
}