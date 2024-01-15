using System;
using UnityEngine;
using Attribute = Actors.Components.Attributes.Attribute;

namespace Actors.Components.Modifiers
{
    public class AttributeModifier : Modifier
    {
        [SerializeField] private Attribute lookForAttribute;
        [SerializeField] private NormalOperations operationInAttribute;
        [SerializeField] private NormalOperations operationWithValue;
        public override Func<Actor, float, float> GetModifier()
        {
            return GetModifiedByAttributeValue;
        }

        private float GetModifiedByAttributeValue(Actor actor, float receivedValue)
        {
            return ExecuteOperation(operationWithValue, receivedValue, 
                ExecuteOperation(operationInAttribute, value, actor.GetAttributeValue(lookForAttribute)));
        }

    }
}