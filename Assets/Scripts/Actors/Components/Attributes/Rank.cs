using System;
using System.Collections.Generic;

namespace Actors.Components.Attributes
{
    public class Rank
    {
        private List<Func<Actor, float, float>> _modifiers = new();
        private float _rankValue;

        public void AddModifier(Func<Actor, float, float> modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(Func<Actor, float, float> modifier)
        {
            _modifiers.Remove(modifier);
        }

        public float CalculateRankValue(Actor actor,float value)
        {
            var newValue = value;
            foreach (var mod in _modifiers)
            {
                newValue = mod(actor, newValue);
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