using UnityEngine;

namespace Actors.Components
{
    public abstract class Component : ScriptableObject 
    {
        public virtual void Setup(Actor actor) { }

        public T GetCopy<T>() where T : Component
        {
            return Instantiate(this) as T;
        }
    }
}