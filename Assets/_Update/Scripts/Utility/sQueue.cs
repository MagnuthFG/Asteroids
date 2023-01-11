using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Magnuth
{
    /// <summary>
    /// Serializable Queueu
    /// </summary>
    public class sQueue<T> : Queue<T>, ISerializationCallbackReceiver
    {
        [SF] private LinkedList<T> _items = null;

        /// <summary>
        /// Add to queue
        /// </summary>
        public new void Enqueue(T item){
            _items.AddLast(item);
        }

        /// <summary>
        /// Remove from queue
        /// </summary>
        public new T Dequeue(){
            var item = _items.First;

            _items.RemoveFirst();
            return item.Value;
        }

        /// <summary>
        /// Saves queue to list
        /// </summary>
        public void OnAfterDeserialize(){
            _items.Clear();

            if (typeof(T) == typeof(Object) ||
                typeof(T).IsSubclassOf(typeof(Object))){

                foreach (var element in this.Where(element => element != null)){
                    _items.AddLast(element);
                }

            } else {
                foreach (var element in this){
                    _items.AddLast(element);
                }
            }
        }

        /// <summary>
        /// Loads queue from list
        /// </summary>
        public void OnBeforeSerialize(){
            var node = _items.First;
            Clear();

            if (typeof(T) == typeof(Object) ||
                typeof(T).IsSubclassOf(typeof(Object))){

                while(node.Next != null){
                    if (node.Value != null){ 
                        Enqueue(node.Value);
                    }
                    node = node.Next;
                }

            } else {
                while (node.Next != null){
                    Enqueue(node.Value);
                    node = node.Next;
                }
            }
        }
    }
}