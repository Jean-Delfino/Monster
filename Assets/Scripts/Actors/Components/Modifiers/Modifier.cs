using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Actors.Components.Modifiers
{
    public enum NormalOperations
    {
        Add,
        Multiply,
        Divide,
    }
    public abstract class Modifier : ScriptableObject
    {
        [SerializeField] private int priority;
        [SerializeField] protected float value;
        public int Priority => priority;

        public abstract Func<Actor, float, float> GetModifier();

        protected float ExecuteOperation(NormalOperations opp, float firstValue, float secondValue)
        {
            switch (opp)
            {
                case NormalOperations.Add:
                    return firstValue + secondValue;
                case NormalOperations.Multiply:
                    return firstValue * secondValue;
                case NormalOperations.Divide:
                    return firstValue / secondValue;
                default:
                    return float.NaN;
            }
        }
    }
}