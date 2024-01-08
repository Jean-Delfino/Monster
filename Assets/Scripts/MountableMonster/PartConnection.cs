﻿using UnityEngine;

namespace MountableMonster
{
    public class PartConnection : MonoBehaviour
    {
        [SerializeField] private Vector3 partRotation = Vector3.zero;
        
        private MonsterPart _connectedPart;

        public void ConnectPart(MonsterPart part)
        {
            SimpleConnect(part);
            
            part.SetConnected(true);
            part.SetRotation(partRotation);
            part.SetPosition(transform.position);
        }

        public void SimpleConnect(MonsterPart part)
        {
            _connectedPart = part;

        }
    }
}