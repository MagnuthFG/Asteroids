using SS = System.SerializableAttribute;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace Magnuth
{
    /// <summary>
    /// Serializable Queueu
    /// </summary>
    [SS] public class sQueue<T> : Queue<T>, ISerializationCallbackReceiver
    {
        [SF] private List<T> _items = new List<T>();
        private bool _object = false;

        /// <summary>
        /// Initialises the Queue
        /// </summary>
        public sQueue() : base(){
            _object = typeof(T) == typeof(Object) ||
                      typeof(T).IsSubclassOf(typeof(Object));
        }

        /// <summary>
        /// Add to queue
        /// </summary>
        public new void Enqueue(T item){
            if (!IsValid(item)) return;

            base.Enqueue(item);

            _items.Add(item);
        }

        /// <summary>
        /// Saves queue to list
        /// </summary>
        public void OnBeforeSerialize(){
            _items.Clear();

            foreach (var item in this){
                if (!IsValid(item)) continue;

                _items.Add(item);
            }
        }

        /// <summary>
        /// Loads queue from list
        /// </summary>
        public void OnAfterDeserialize(){
            Clear();

            foreach (var item in _items){
                if (!IsValid(item)) continue;

                base.Enqueue(item);
            }
        }

        /// <summary>
        /// Returns if item is not null
        /// </summary>
        private bool IsValid(T item){
            return !_object || (_object && item != null);
        }
    }
}