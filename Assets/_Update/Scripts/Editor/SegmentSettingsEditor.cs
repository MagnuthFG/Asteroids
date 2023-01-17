using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Magnuth
{
    [CustomEditor(typeof(SegmentSettings))]
    public class SegmentSettingsEditor : Editor 
    {
        private VisualTreeAsset _vtree = null;

// INITIALISATION

        /// <summary>
        /// Initialises the inspector
        /// </summary>
        private void OnEnable(){
            _vtree = LoadTreeAsset();
        }

        /// <summary>
        /// Loads the visual tree asset from this file directory
        /// </summary>
        private VisualTreeAsset LoadTreeAsset(){
            var script = MonoScript.FromScriptableObject(this);
            var path   = AssetDatabase.GetAssetPath(script);

            var filepath = new FileInfo(path).ToString();
            filepath = filepath.Substring(0, filepath.Length - 2);

            return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                $"{filepath}uxml"
            );
        }

// INTERFACE

        /// <summary>
        /// Draws the new inspector interface
        /// </summary>
        public override VisualElement CreateInspectorGUI(){
            //return base.CreateInspectorGUI();

            var root = new VisualElement();
            _vtree.CloneTree(root);
            
            return root;
        }
    }
}