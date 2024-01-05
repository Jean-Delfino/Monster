using System;
using UnityEngine;

namespace Actor.Components.Modifiers
{
    public abstract class Modifier : ScriptableObject
    {
        [SerializeField] private int priority;

        public int Priority => priority;

        public abstract Func<float, float> GetModifier();
    }
}