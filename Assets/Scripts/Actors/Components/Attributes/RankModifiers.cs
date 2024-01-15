using System;
using System.Collections.Generic;
using System.Linq;

namespace Actors.Components.Attributes
{
    public class RankModifiers
    {
        private HashSet<Func<float, float>> _constantlyChangingModifiers = new();
        private SortedDictionary<int, Rank> _modifiers = new();

        public void AddConstantlyChangingModifier(Func<float, float> modifier)
        {
            _constantlyChangingModifiers.Add(modifier);
        }
        
        public void RemoveConstantlyChangingModifier(Func<float, float> modifier)
        {
            _constantlyChangingModifiers.Remove(modifier);
        }

        public float ProcessWithConstantlyChangingModifiers(float value)
        {
            if (_constantlyChangingModifiers.Count == 0) return value;
            
            var res = value;
            foreach (var ccModifier in _constantlyChangingModifiers)
            {
                res = ccModifier(res);
            }

            return res;
        }

        public float AddModifier(float initialValue, int priority, Func<float,float> modifier)
        {
            if(!_modifiers.ContainsKey(priority)) _modifiers.Add(priority, new Rank());
            
            _modifiers[priority].AddModifier(modifier);
            return RefreshModifiersInPriority(initialValue, priority);
        }
        
        public float RemoveModifier(float initialValue, float defaultValue, int priority, Func<float, float> modifier)
        {
            if(!_modifiers.ContainsKey(priority)) return defaultValue;
            
            _modifiers[priority].RemoveModifier(modifier);
            return RefreshModifiersInPriority(initialValue, priority);
        }
        
        private float RefreshModifiersInPriority(float initialValue, int priority)
        {
            var array = _modifiers.Keys.ToArray(); //This could be saved in memory, but I want to preserve more memory in exchange of performance
            var index = GetPriorityIndex(priority, array); //Priority is always in the array
            
            return ReCalculateValuesFromPriorityRank(initialValue, array, index);
        }
        
        private int GetPriorityIndex(int priority, int[] array)
        {
            return Array.BinarySearch(array, priority);
        }
        private float GetPreviousRankValue(int index, float value)
        {
            return index > 0 ? _modifiers[index - 1].GetCalculateRankValue() : value;
        }

        private float ReCalculateValuesFromPriorityRank(float initialValue, int[] keys, int priorityIndexInKeys)
        {
            var lastValue = GetPreviousRankValue(priorityIndexInKeys, initialValue);

            for (int i = priorityIndexInKeys; i < keys.Length; i++)
            {
                lastValue = _modifiers[keys[i]].CalculateRankValue(lastValue);
            }

            return lastValue;
        }
    }
}