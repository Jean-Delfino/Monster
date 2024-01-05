using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Actor.Components.Attributes
{
    [Serializable]
    public class AttributeGroup
    {
        [SerializeField] private Attribute attribute;
        [SerializeField] private float value;

        public Attribute Attribute => attribute;
        private float _attributeValue;
        
        private SortedDictionary<int, Rank> _modifiers = new();
        private HashSet<Func<float, float>> _constantlyChangingModifiers = new(); 
        
        public void Setup()
        {
            _attributeValue = value;
        }

        public void AddModifier(int priority, Func<float, float> modifier)
        {
            if(!_modifiers.ContainsKey(priority)) _modifiers.Add(priority, new Rank());
            
            _modifiers[priority].AddModifier(modifier);
            RefreshModifiersInPriority(priority);
        }

        public void RemoveModifier(int priority, Func<float, float> modifier)
        {
            if(!_modifiers.ContainsKey(priority)) return;
            
            _modifiers[priority].RemoveModifier(modifier);
            RefreshModifiersInPriority(priority);
        }

        public void AddConstantlyChangingModifier(Func<float, float> modifier)
        {
            _constantlyChangingModifiers.Add(modifier);
        }

        public void RemoveConstantlyChangingModifier(Func<float, float> modifier)
        {
            _constantlyChangingModifiers.Remove(modifier);
        }
        
        private void RefreshModifiersInPriority(int priority)
        {
            var array = _modifiers.Keys.ToArray(); //This could be saved in memory, but I want to preserve more memory in exchange of performance
            var index = GetPriorityIndex(priority, array); //Priority is always in the array
            
            ReCalculateValuesFromPriorityRank(array, index);
        }
        
        private int GetPriorityIndex(int priority, int[] array)
        {
            return Array.BinarySearch(array, priority);
        }
        private float GetPreviousRankValue(int index)
        {
            return index > 0 ? _modifiers[index - 1].GetCalculateRankValue() : value;
        }

        private void ReCalculateValuesFromPriorityRank(int[] keys, int priorityIndexInKeys)
        {
            var lastValue = GetPreviousRankValue(priorityIndexInKeys);

            for (int i = priorityIndexInKeys; i < keys.Length; i++)
            {
                lastValue = _modifiers[keys[i]].CalculateRankValue(lastValue);
            }

            _attributeValue = lastValue;
        }

        public float GetValue()
        {
            return _attributeValue;
        }

        public float GetValueAfterConstantlyChangingModifiers()
        {
            if (_constantlyChangingModifiers.Count == 0) return _attributeValue;
            
            var res = _attributeValue;
            foreach (var ccModifier in _constantlyChangingModifiers)
            {
                res = ccModifier(res);
            }

            return res;
        }
    }
}