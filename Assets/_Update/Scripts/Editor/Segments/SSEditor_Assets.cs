using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth {
    public sealed partial class SegmentSettingsEditor : Editor
    {
        /// <summary>
        /// Creates a new scriptable object asset and parents it to this asset
        /// </summary>
        private void Create<T>(SerializedProperty property) where T : ScriptableObject {
            if (property.objectReferenceValue != null) return;
            var asset = AssetUtility.CreateSubAsset<T>(_target);

            property.objectReferenceValue = asset;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }

        /// <summary>
        /// Deletes the scriptable object asset from its parrent asset
        /// </summary>
        private void Delete(SerializedProperty property){
            if (property.objectReferenceValue == null ||
                !AssetUtility.RemoveAssetPopup()) 
                return;

            var obj = property.objectReferenceValue;
            AssetUtility.TrashSubAsset((ScriptableObject)obj);

            property.objectReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
    }
}