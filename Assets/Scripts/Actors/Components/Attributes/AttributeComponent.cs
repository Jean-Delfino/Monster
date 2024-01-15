using System;
using System.Collections.Generic;
using Actors.Components.Modifiers;
using UnityEngine;

namespace Actors.Components.Attributes
{
    [CreateAssetMenu(menuName = "Monster/Game-Design/Components/Attributes Component")]
    public class AttributeComponent : Component
    {
        [Serializable]
        public class AttributeDefinition
        {
#if UNITY_EDITOR
            public string name;
#endif
            
            public AttributeGroup groupAttribute;
            public float minValue;
            public float maxValue;

            public float GetAttributeRestrictedValue(float value)
            {
                return Mathf.Clamp(value, minValue, maxValue);
            }
        }
        
        
        [SerializeField] protected AttributeDefinition[] baseAttributes;
        private readonly Dictionary<Attribute, AttributeDefinition> _attributesLookup = new();

        public override void Setup()
        {
            foreach (var description in baseAttributes)
            {
                var att = description.groupAttribute.Attribute;
                if (_attributesLookup.ContainsKey(att)) continue;

                description.groupAttribute.Setup();
                _attributesLookup.Add(att, description);
            }
        }
        
        public float GetAttributeValue(Attribute attribute)
        {
            if (!_attributesLookup.ContainsKey(attribute)) return -1;

            var groupValue = _attributesLookup[attribute].groupAttribute;
            var value = groupValue.GetValueAfterConstantlyChangingModifiers();

            if (value != groupValue.GetValue()) SetAttributesDependents(attribute);
            
            return value;
        }

        public void AddModifier(Attribute attribute, Modifier modifier)
        {
            if (!_attributesLookup.ContainsKey(attribute)) return;

            var groupValue =  _attributesLookup[attribute].groupAttribute;
            ExecuteModifierChange(groupValue.AddModifier, groupValue, attribute, modifier);
        }

        public void RemoveModifier(Attribute attribute, Modifier modifier)
        {
            if (!_attributesLookup.ContainsKey(attribute)) return;
            
            var groupValue =  _attributesLookup[attribute].groupAttribute;
            ExecuteModifierChange(groupValue.RemoveModifier, groupValue, attribute, modifier);
        }

        private void ExecuteModifierChange(Action<int, Func<float, float>> action, AttributeGroup group, Attribute attribute, Modifier modifier)
        {
            var value = group.GetValue();
            
            action.Invoke(modifier.Priority, modifier.GetModifier());

            if (value != group.GetValue()) SetAttributesDependents(attribute);
        }

        private void SetAttributesDependents(Attribute att)
        {
            
        }
        
        private void SetAllAttributesDependents()
        {
        }
    }
}