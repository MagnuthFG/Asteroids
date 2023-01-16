using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth
{
    [CustomEditor(typeof(SegmentSettings))]
    public class SegmentSettingsEditor : Editor 
    {
        /// <summary>
        /// 
        /// </summary>
        private void OnEnable(){
            
        }

        /// <summary>
        /// 
        /// </summary>
        public override VisualElement CreateInspectorGUI(){
            return base.CreateInspectorGUI();
        }
    }
}