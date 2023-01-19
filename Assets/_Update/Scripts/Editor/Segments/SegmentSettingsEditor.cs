using UnityEngine.UIElements;
using UnityEditor;

namespace Magnuth
{
    [CustomEditor(typeof(SegmentSettings))]
    public sealed partial class SegmentSettingsEditor : Editor 
    {
        private SerializedProperty 
            _waves = null,
            _asteroids = null;

        private SegmentSettings _target = null;
        private VisualTreeAsset _vtree = null;

// INITIALISATION

        /// <summary>
        /// Initialises the inspector
        /// </summary>
        private void OnEnable(){
            _target    = (SegmentSettings)target;
            _asteroids = serializedObject.FindProperty("_asteroids");
            _waves     = serializedObject.FindProperty("_waves");

            _vtree = AssetUtility.LoadTreeAsset(this);
        }

        /// <summary>
        /// Initialises the inspector interface
        /// </summary>
        public override VisualElement CreateInspectorGUI(){
            if (_vtree == null) return base.CreateInspectorGUI();

            var root = new VisualElement();
            _vtree.CloneTree(root);

            InitTabs(root);
            InitAssetFieldsButtons(root);

            return root;
        }
    }
}