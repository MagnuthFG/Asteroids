using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth {
    public sealed partial class AsteroidSettingsEditor : Editor
    {
        /// <summary>
        /// Initialises the asset name field and buttons
        /// </summary>
        private void InitAssetNaming(VisualElement root){
            var field  = root.Query<TextField>("AssetNameField").First();
            var button = root.Query<Button>("AssetRenameButton").First();

            field.value = _target.name;

            button.clicked += () => {
                AssetUtility.RenameSubAsset(_target, field.value);
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
                AssetUtility.TrashSubAsset(_target);
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