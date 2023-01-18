using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

namespace Magnuth {
    public sealed partial class SegmentSettingsEditor : Editor
    {
        private const string TAB_SELECTED = "Tab-Selected";

// TABS INTERFACE

        /// <summary>
        /// Initialises the tab buttons and elements
        /// </summary>
        private void InitTabs(VisualElement root){
            var wTab     = root.Query<VisualElement>("WavesTab").First();
            var aTab     = root.Query<VisualElement>("AsteroidsTab").First();
            var wContent = root.Query<VisualElement>("WavesContent").First();
            var aContent = root.Query<VisualElement>("AsteroidsContent").First();

            SetTabBehaviour(wTab, wContent, aTab, aContent);
            SetTabBehaviour(aTab, aContent, wTab, wContent);
        }

        /// <summary>
        /// Assigns the tab buttons and elements behaviours
        /// </summary>
        private void SetTabBehaviour(VisualElement focusTab, VisualElement focusContent,
        VisualElement otherTab, VisualElement otherContent){
            focusTab.AddManipulator(new Clickable(e => {
                if (focusContent.visible) return;

                otherContent.style.display = DisplayStyle.None;
                otherContent.style.visibility = Visibility.Hidden;
                otherTab.RemoveFromClassList(TAB_SELECTED);

                focusContent.style.display = DisplayStyle.Flex;
                focusContent.style.visibility = Visibility.Visible;
                focusTab.AddToClassList(TAB_SELECTED);
            }));
        }

// ASSETS INTERFACE

        /// <summary>
        /// Initialises the asset fields and buttons
        /// </summary>
        private void InitAssetFieldsButtons(VisualElement root){
            var wCreate = root.Query<Button>("CreateWaveButton").First();
            var wDelete = root.Query<Button>("DeleteWaveButton").First();
            var wField  = root.Query<PropertyField>("WaveSettingsProperty").First();

            SetAssetBehaviour<WaveSettings>(
                wCreate, wDelete, wField, _waves
            );

            var aCreate = root.Query<Button>("CreateAsteroidGroupButton").First();
            var aDelete = root.Query<Button>("DeleteAsteroidGroupButton").First();
            var aField  = root.Query<PropertyField>("AsteroidGroupProperty").First();

            SetAssetBehaviour<AsteroidGroupSettings>(
                aCreate, aDelete, aField, _asteroids
            );
        }

        /// <summary>
        /// Assigns the asset fields and buttons behaviours
        /// </summary>
        private void SetAssetBehaviour<T>(Button create, Button delete,
        PropertyField field, SerializedProperty property) where T : ScriptableObject {
            create.clicked += () => { Create<T>(property); };
            delete.clicked += () => { Delete<T>(property); };

            field.RegisterValueChangeCallback(e => {
                bool assigned = property.objectReferenceValue != null;

                create.style.display = assigned ?
                    DisplayStyle.None :
                    DisplayStyle.Flex;

                delete.style.display = assigned ?
                    DisplayStyle.Flex :
                    DisplayStyle.None;
            });
        }
    }
}