using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth {
    public sealed partial class AsteroidGroupEditor : Editor
    {
        /// <summary>
        /// Initialises the asteroid settings buttons
        /// </summary>
        private void InitSettingsButtons(VisualElement root){
            var list = root.Query<ListView>("AsteroidSettingsList").First();

            InitSettingsList(root, list);

            InitAddButton(root, list);
            InitRemoveButton(root, list);
            
            InitCreateButton(root, list);
            InitDeleteButton(root, list);
        }

        /// <summary>
        /// Initialises the asteroid settings list elements
        /// </summary>
        private void InitSettingsList(VisualElement root, ListView list){
            var sList = root.Query<ListView>("AsteroidSettingsList").First();

            sList.makeItem = () => {
                var item = new VisualElement();

                item.Add(new PropertyField(){ 
                    label = string.Empty 
                });

                return item;
            };
        }

        /// <summary>
        /// Initialises the asteroid settings list add button
        /// </summary>
        private void InitAddButton(VisualElement root, ListView list){
            var button = root.Query<Button>("SettingsAddButton").First();

            button.clicked += () => {
                _settings.arraySize++;

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            };
        }

        /// <summary>
        /// Initialises the asteroid settings list remove button
        /// </summary>
        private void InitRemoveButton(VisualElement root, ListView list){
            var button = root.Query<Button>("SettingsRemoveButton").First();

            button.clicked += () => {
                if (list.selectedIndex < 0) return;

                _settings.DeleteArrayElementAtIndex(
                    list.selectedIndex
                );

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            };
        }

        /// <summary>
        /// Initialises the asteroid settings list create button
        /// </summary>
        private void InitCreateButton(VisualElement root, ListView list){
            var button = root.Query<Button>("SettingsCreateButton").First();

            button.clicked += () => {
                var asset = AssetUtility.CreateSubAsset
                    <AsteroidSettings>(_target);

                _settings.arraySize++;

                var index = list.selectedIndex < 0 ? 
                    _settings.arraySize - 1 : 
                    list.selectedIndex;

                var element = _settings.GetArrayElementAtIndex(index);
                element.objectReferenceValue = asset;

                element.serializedObject.ApplyModifiedProperties();
                element.serializedObject.Update();
            };
        }

        /// <summary>
        /// Initialises the asteroid settings list delete button
        /// </summary>
        private void InitDeleteButton(VisualElement root, ListView list){
            var button = root.Query<Button>("SettingsDeleteButton").First();

            button.clicked += () => {
                if (list.selectedIndex < 0 ||
                    !AssetUtility.RemoveAssetPopup())
                    return;

                var element = _settings.GetArrayElementAtIndex(
                    list.selectedIndex
                );

                element.objectReferenceValue = null;
                _settings.DeleteArrayElementAtIndex(
                    list.selectedIndex
                );

                var obj = element.objectReferenceValue;
                AssetUtility.TrashSubAsset((ScriptableObject)obj);

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            };
        }


        /// <summary>
        /// Initialises the asset name field and buttons
        /// </summary>
        private void InitAssetNaming(VisualElement root){
            var nField  = root.Query<TextField>("AssetNameField").First();
            var nButton = root.Query<Button>("AssetRenameButton").First();

            nField.value = _target.name;

            nButton.clicked += () => {
                _target.name = nField.value;
                
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