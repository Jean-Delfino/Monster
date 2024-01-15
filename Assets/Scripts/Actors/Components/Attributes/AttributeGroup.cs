using System;
using UnityEngine;

namespace Actors.Components.Attributes
{
    [Serializable]
    public class AttributeGroup
    {
        [SerializeField] private Attribute attribute;
        [SerializeField] private float value;

        public Attribute Attribute => attribute;
        private float _attributeValue;
        
        private RankModifiers _rankModifiers = new();
        public void Setup()
        {
            _attributeValue = value;
        }

        public void AddModifier(int priority, Func<float, float> modifier)
        {
            _attributeValue = _rankModifiers.AddModifier(value, priority, modifier);
        }

        public void RemoveModifier(int priority, Func<float, float> modifier)
        {
            _attributeValue = _rankModifiers.RemoveModifier(value,_attributeValue, priority, modifier);
        }

        public void AddConstantlyChangingModifier(Func<float, float> modifier)
        {
            _rankModifiers.AddConstantlyChangingModifier(modifier);
        }

        public void RemoveConstantlyChangingModifier(Func<float, float> modifier)
        {
            _rankModifiers.RemoveConstantlyChangingModifier(modifier);
        }
        
        public float GetValue()
        {
            return _attributeValue;
        }

        public float GetValueAfterConstantlyChangingModifiers()
        {
            return _rankModifiers.ProcessWithConstantlyChangingModifiers(_attributeValue);
        }
    }
}