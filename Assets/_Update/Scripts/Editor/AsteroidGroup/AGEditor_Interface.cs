using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth {
    public sealed partial class AsteroidGroupEditor : Editor
    {
        /// <summary>
        /// Initialises the asset name field and buttons
        /// </summary>
        private void InitAssetNaming(VisualElement root){
            var nfield  = root.Query<TextField>("AssetNameField").First();
            var nbutton = root.Query<Button>("AssetRenameButton").First();

            nfield.value = _target.name;

            nbutton.clicked += () => {
                _target.name = nfield.value;
                
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            };
        }

        /// <summary>
        /// Initialises the asset option buttons
        /// </summary>
        private void InitAssetOptions(VisualElement root){ 
            var uButton = root.Query<Button>("AssetUnparentButton").First();
            var dButton = root.Query<Button>("AssetDeleteButton").First();

            uButton.clicked += () => {
                AssetUtility.RemoveSubAsset(_target);
            };

            dButton.clicked += () => {
                AssetUtility.TrashAsset(_target);
            };
        }

        /// <summary>
        /// Hides the asset naming and options panels
        /// </summary>
        private void HideAssetPanels(VisualElement root){
            var nPanel = root.Query<VisualElement>("AssetNamePanel").First();
            var oPanel = root.Query<VisualElement>("AssetOptionsPanel").First();

            nPanel.style.display = DisplayStyle.None;
            oPanel.style.display = DisplayStyle.None;
        }
    }
}