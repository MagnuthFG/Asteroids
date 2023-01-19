using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth
{
    [CustomEditor(typeof(AsteroidGroup))]
    public sealed partial class AsteroidGroupEditor : Editor
    {
        private bool _subAsset = false;
        private AsteroidGroup   _target = null;
        private VisualTreeAsset _vtree = null;

// INITIALISATION

        /// <summary>
        /// Initialises the inspector
        /// </summary>
        private void OnEnable(){
            _target   = (AsteroidGroup)target;
            _subAsset = AssetDatabase.IsSubAsset(_target);
            _vtree    = AssetUtility.LoadTreeAsset(this);
        }

        /// <summary>
        /// Initialises the inspector interface
        /// </summary>
        public override VisualElement CreateInspectorGUI(){
            if (_vtree == null) return base.CreateInspectorGUI();

            var root = new VisualElement();
            _vtree.CloneTree(root);

            if (_subAsset){ 
                InitAssetNaming(root);
                InitAssetOptions(root);
            
            } else HideAssetPanels(root);

            return root;
        }
    }
}