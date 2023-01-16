using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;

namespace Magnuth
{
    [CustomPropertyDrawer(typeof(sQueue<>))]
    public class sQueueDrawer : PropertyDrawer
    {
        /// <summary>
        /// Draws the serialized queue items as a reorderable list
        /// </summary>
        public override VisualElement CreatePropertyGUI(SerializedProperty property){
            var root = new VisualElement();
            var list = new ListView();

            var items = property.FindPropertyRelative("_items");
            list.bindingPath = items.propertyPath;
            list.headerTitle = property.displayName;

            list.showFoldoutHeader   = true;
            list.showAddRemoveFooter = true;
            list.showBorder  = true;
            list.reorderable = true;
            list.reorderMode = ListViewReorderMode.Animated;

            list.makeItem = () => {
                var item = new VisualElement();

                item.Add(new PropertyField(){
                    label = string.Empty
                });

                return item;
            };

            root.Add(list);
            return root;
        }
    }
}