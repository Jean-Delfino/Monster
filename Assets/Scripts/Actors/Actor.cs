using System;
using System.Collections;
using Actors.Components;
using Actors.Components.Attributes;
using UnityEngine;
using UnityEngine.AI;
using Attribute = Actors.Components.Attributes.Attribute;

namespace Actors
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private BehaviorComponent brainComponent;
        [SerializeField] private AttributeComponent attributeComponent;
        
        [Space] [Header("NAV MESH")] [Space]
        [SerializeField] private NavMeshAgent agent;

        public NavMeshAgent Agent => agent;
        public float GetAttributeValue(Attribute attribute)
        {
            return attributeComponent.GetAttributeValue(this, attribute);
        }

        private void Awake()
        {
            brainComponent = brainComponent.GetCopy<BehaviorComponent>();
            attributeComponent = attributeComponent.GetCopy<AttributeComponent>();
        }

        private void Start()
        {
            brainComponent.Setup(this);
            attributeComponent.Setup(this);

            StartCoroutine(brainComponent.ExecuteComponent(this));
        }

    }
}