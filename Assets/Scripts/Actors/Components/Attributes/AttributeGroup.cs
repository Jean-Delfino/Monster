using System;
using Actors.Components.Modifiers;
using UnityEngine;
using Object = System.Object;

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

        public void AddModifier(Actor actor, int priority, Func<Actor, float, float> modifier)
        {
            _attributeValue = _rankModifiers.AddModifier(actor, value, priority, modifier);
        }

        public void RemoveModifier(Actor actor, int priority, Func<Actor, float, float> modifier)
        {
            _attributeValue = _rankModifiers.RemoveModifier(actor, value,_attributeValue, priority, modifier);
        }

        public void AddConstantlyChangingModifier(Func<Actor, float, float> modifier)
        {
            _rankModifiers.AddConstantlyChangingModifier(modifier);
        }

        public void RemoveConstantlyChangingModifier(Func<Actor, float, float> modifier)
        {
            _rankModifiers.RemoveConstantlyChangingModifier(modifier);
        }

        public void RefreshAt(Actor actor, int priority)
        {
            _rankModifiers.RefreshModifiersInPriority(actor, value, priority);
        }
        
        public float GetValue()
        {
            return _attributeValue;
        }

        public float GetValueAfterConstantlyChangingModifiers(Actor actor)
        {
            return _rankModifiers.ProcessWithConstantlyChangingModifiers(actor, _attributeValue);
        }
    }
}