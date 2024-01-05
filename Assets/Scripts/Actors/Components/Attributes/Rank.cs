using System;
using System.Collections.Generic;

namespace Actors.Components.Attributes
{
    public class Rank
    {
        private List<Func<float, float>> _modifiers = new();
        private float _rankValue;

        public void AddModifier(Func<float,float> modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(Func<float, float> modifier)
        {
            _modifiers.Remove(modifier);
        }

        public float CalculateRankValue(float value)
        {
            var newValue = value;
            foreach (var mod in _modifiers)
            {
                newValue = mod(newValue);
            }

            _rankValue = newValue;
            return _rankValue;
        }

        public float GetCalculateRankValue()
        {
            return _rankValue;
        }
        
    }
}