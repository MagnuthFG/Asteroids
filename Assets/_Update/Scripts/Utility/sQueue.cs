using SS = System.SerializableAttribute;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace Magnuth
{
    /// <summary>
    /// Serializable Queue
    /// </summary>
    [SS] public class sQueue<T> : Queue<T>, ISerializationCallbackReceiver
    {
        [SF] private List<T> _items = new List<T>();

        /// <summary>
        /// Saves queue to list
        /// </summary>
        public void OnBeforeSerialize(){
            _items.Clear();

            if (Count == 0) 
                return;

            foreach (var item in this){
                _items.Add(item);
            }
        }

        /// <summary>
        /// Loads queue from list
        /// </summary>
        public void OnAfterDeserialize(){
            Clear();

            for (int i = 0; i < _items.Count; i++){
                base.Enqueue(_items[i]);
            }
        }
    }
}