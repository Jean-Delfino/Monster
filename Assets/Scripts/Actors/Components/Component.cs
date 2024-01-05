using UnityEngine;

namespace Actors.Components
{
    public abstract class Component : ScriptableObject 
    {
        public virtual void Setup() { }

        public T GetCopy<T>() where T : Component
        {
            return Instantiate(this) as T;
        }
    }
}