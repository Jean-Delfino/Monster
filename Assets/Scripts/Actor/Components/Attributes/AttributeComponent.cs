using System;
using System.Collections.Generic;
using Actor.Components.Modifiers;
using UnityEngine;

namespace Actor.Components.Attributes
{
    [CreateAssetMenu(menuName = "Game Design/Components/Attributes Component")]
    public class AttributeComponent : ScriptableObject
    {
        [Serializable]
        public class AttributeDefinition
        {
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


        public float GetAttributeValue(Attribute attribute)
        {
            if (_attributesLookup.ContainsKey(attribute)) return -1;

            var groupValue = _attributesLookup[attribute].groupAttribute;
            var value = groupValue.GetValueAfterConstantlyChangingModifiers();

            if (value != groupValue.GetValue()) SetAttributesDependents(attribute);
            
            return value;
        }
        
        public void AttributesSetup()
        {
            foreach (var description in baseAttributes)
            {
                var att = description.groupAttribute.Attribute;
                if (_attributesLookup.ContainsKey(att)) continue;

                _attributesLookup.Add(att, description);
            }
        }

        public void AddModifier(Attribute attribute, Modifier modifier)
        {
            if (_attributesLookup.ContainsKey(attribute)) return;

            var groupValue =  _attributesLookup[attribute].groupAttribute;
            var value = groupValue.GetValue();
            
            groupValue.AddModifier(modifier.Priority, modifier.GetModifier());

            if (value != groupValue.GetValue()) SetAttributesDependents(attribute);
        }

        public void RemoveModifier(Attribute attribute, Modifier modifier)
        {
            if (_attributesLookup.ContainsKey(attribute)) return;
            
            var groupValue =  _attributesLookup[attribute].groupAttribute;
            var value = groupValue.GetValue();
            
            groupValue.RemoveModifier(modifier.Priority, modifier.GetModifier());

            if (value != groupValue.GetValue()) SetAttributesDependents(attribute);
        }

        private void SetAttributesDependents(Attribute att)
        {
            
        }
        
        private void SetAllAttributesDependents()
        {
        }
    }
}