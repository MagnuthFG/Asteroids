using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth {
    public sealed partial class AsteroidGroupEditor : Editor
    {
// SETTINGS

        /// <summary>
        /// Initialises the asteroid settings list and buttons
        /// </summary>
        private void InitSettings(VisualElement root){
            var list = root.Query<ListView>("AsteroidSettingsList").First();

            InitSettingsList(root, list);

            InitSettingsAddButton(root, list);
            InitSettingsRemoveButton(root, list);
            
            InitSettingsCreateButton(root, list);
            InitSettingsDeleteButton(root, list);
        }

        /// <summary>
        /// Initialises the asteroid settings list elements
        /// </summary>
        private void InitSettingsList(VisualElement root, ListView list){
            list.makeItem = () => {
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
        private void InitSettingsAddButton(VisualElement root, ListView list){
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
        private void InitSettingsRemoveButton(VisualElement root, ListView list){
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
        private void InitSettingsCreateButton(VisualElement root, ListView list){
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
        private void InitSettingsDeleteButton(VisualElement root, ListView list){
            var button = root.Query<Button>("SettingsDeleteButton").First();

            button.clicked += () => {
                if (list.selectedIndex < 0 ||
                    !AssetUtility.RemoveAssetPopup())
                    return;

                var index   = list.selectedIndex;
                var element = _settings.GetArrayElementAtIndex(index);
                var obj     = element.objectReferenceValue;
                
                element.objectReferenceValue = null;
                _settings.DeleteArrayElementAtIndex(index);

                // delayed to prevent array serialisation errors
                AssetUtility.DelayedTrashSubAsset((ScriptableObject)obj);

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            };
        }

// PREFABS

        /// <summary>
        /// Initialises the asteroid prefab list and buttons
        /// </summary>
        private void InitPrefabs(VisualElement root){
            var list = root.Query<ListView>("AsteroidPrefabsList").First();

            InitPrefabList(root, list);
            InitPrefabAddButton(root, list);
            InitPrefabRemoveButton(root, list);
        }

        /// <summary>
        /// Initialises the asteroid prefabs list elements
        /// </summary>
        private void InitPrefabList(VisualElement root, ListView list){
            list.makeItem = () => {
                var item = new VisualElement();

                item.Add(new PropertyField(){
                    label = string.Empty
                });

                return item;
            };
        }

        /// <summary>
        /// Initialises the asteroid prefab list add button
        /// </summary>
        private void InitPrefabAddButton(VisualElement root, ListView list){
            var button = root.Query<Button>("PrefabsAddButton").First();

            button.clicked += () => {
                _prefabs.arraySize++;

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            };
        }

        /// <summary>
        /// Initialises the asteroid prefab list remove button
        /// </summary>
        private void InitPrefabRemoveButton(VisualElement root, ListView list){
            var button = root.Query<Button>("PrefabsRemoveButton").First();

            button.clicked += () => {
                if (list.selectedIndex < 0) return;

                _prefabs.DeleteArrayElementAtIndex(
                    list.selectedIndex
                );

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            };
        }

// ASSET

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