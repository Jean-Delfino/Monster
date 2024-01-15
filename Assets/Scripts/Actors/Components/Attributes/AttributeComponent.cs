using System;
using System.Collections.Generic;
using Actors.Components.Modifiers;
using Unity.VisualScripting;
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

        [Serializable]
        public class DependentAttribute
        {
#if UNITY_EDITOR
            public string name;
#endif
            public Attribute attribute;
            public AttributeModifier modifier;
        }

        private class DependentDefinition
        {
            public int MinPriority;
            public Attribute Attribute;
        }

        [SerializeField] protected AttributeDefinition[] baseAttributes;

        [SerializeField] private DependentAttribute[] dependentAttributes;
        
        private readonly Dictionary<Attribute, AttributeDefinition> _attributesLookup = new();
        private readonly Dictionary<Attribute, List<DependentDefinition>> _dependentAttributes = new();

        public override void Setup(Actor actor)
        {
            foreach (var description in baseAttributes)
            {
                var att = description.groupAttribute.Attribute;
                if (_attributesLookup.ContainsKey(att)) continue;

                description.groupAttribute.Setup();
                _attributesLookup.Add(att, description);
            }

            SetAllAttributesDependents(actor);
        }
        
        public float GetAttributeValue(Actor actor, Attribute attribute)
        {
            if (!_attributesLookup.ContainsKey(attribute)) return -1;

            var groupValue = _attributesLookup[attribute].groupAttribute;
            var value = groupValue.GetValueAfterConstantlyChangingModifiers(actor);

            if (value != groupValue.GetValue()) SetAttributesDependents(actor, attribute);
            
            return value;
        }

        public void AddModifier(Actor actor, Attribute attribute, Modifier modifier)
        {
            if (!_attributesLookup.ContainsKey(attribute)) return;

            var groupValue =  _attributesLookup[attribute].groupAttribute;
            ExecuteModifierChange(groupValue.AddModifier, actor, groupValue, attribute, modifier);
        }

        public void RemoveModifier(Actor actor, Attribute attribute, Modifier modifier)
        {
            if (!_attributesLookup.ContainsKey(attribute)) return;
            
            var groupValue =  _attributesLookup[attribute].groupAttribute;
            ExecuteModifierChange(groupValue.RemoveModifier, actor, groupValue, attribute, modifier);
        }

        private void ExecuteModifierChange(Action<Actor, int, Func<Actor, float, float>> action, Actor actor, AttributeGroup group, Attribute attribute, Modifier modifier)
        {
            var value = group.GetValue();
            
            action.Invoke(actor, modifier.Priority, modifier.GetModifier());

            if (value != group.GetValue()) SetAttributesDependents(actor, attribute);
        }

        private void SetAttributesDependents(Actor actor, Attribute att)
        {
            if(!_dependentAttributes.ContainsKey(att)) return;
            
            foreach (var dependent in _dependentAttributes[att])
            {
                RefreshAttribute(actor, dependent.Attribute, dependent.MinPriority);
            }
        }

        private void RefreshAttribute(Actor actor, Attribute attribute, int priority)
        {
            if (!_attributesLookup.ContainsKey(attribute)) return;
            
            _attributesLookup[attribute].groupAttribute.RefreshAt(actor,priority);
        }
        
        private void SetAllAttributesDependents(Actor actor)
        {
            foreach (var dependent in dependentAttributes)
            {
                var lookForAttribute = dependent.modifier.LookForAttribute;
                AddModifier(actor, dependent.attribute,dependent.modifier);
                
                if(!_dependentAttributes.ContainsKey(lookForAttribute)) _dependentAttributes.Add(lookForAttribute, new());
                
                var dependentDefinition = _dependentAttributes[lookForAttribute];
                var attributeDependent = dependentDefinition.Find(e => e.Attribute == dependent.attribute);
                
                if(attributeDependent == null) 
                {
                    dependentDefinition.Add(new DependentDefinition()
                    {
                        MinPriority = dependent.modifier.Priority,
                        Attribute = dependent.attribute
                    });
                }
                else
                {
                    attributeDependent.MinPriority =
                        Mathf.Min(attributeDependent.MinPriority, dependent.modifier.Priority);
                }
            }

            foreach (var att in _dependentAttributes)
            {
                SetAttributesDependents(actor, att.Key);
            }
        }
    }
}