using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CustomHelpers
{
    public class ObjectsCollection<T>
    {
        protected readonly List<T> objects = new();

        public UnityEvent noObjectsLeft = new();
        
        public int CollectionSize => objects.Count;

        public List<T> Collection => objects;

        public virtual void AddUnit(T unit)
        {
            objects.Add(unit);
        }
        
        public virtual void RemoveUnit(T unit)
        {
            if (!objects.Contains(unit))
            {
                Debug.LogWarning($"Unit isn't contains in list");
                return;
            }
            objects.Remove(unit);
        }
    }
}