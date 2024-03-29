﻿using Reuse.Utils;
using UnityEngine;

namespace MountableMonster
{
    public class MonsterPart : MonoBehaviour
    {
        [SerializeField] private PartConnection[] connections;
        [SerializeField] private Renderer[] visualComponents;
        
        public PartConnection[] Connections => connections;
        public Renderer[] VisualComponents => visualComponents;

        private bool _isConnected = false;
        public void GetConnectedToMonsterPart(int connectionIndex, MonsterPart monsterPart, int monsterPartConnection)
        {
            GetConnectedToMonsterPart(connections[connectionIndex], monsterPart, monsterPart.connections[monsterPartConnection]);
        }

        public void GetConnectedToMonsterPart(PartConnection connection, MonsterPart monsterPart, PartConnection monsterPartConnection)
        {
            SimpleConnect(monsterPart, connection); //This getting connected with monster part
            SimpleConnect(this, monsterPartConnection); //Monster part getting connected with this
            
            monsterPart.SetConnectionFacingConnection(connection, monsterPartConnection);
        }
        private void SimpleConnect(MonsterPart monsterPart, PartConnection connection)
        {
            connection.ConnectPart(monsterPart);
        }

        private void SetConnectionFacingConnection(PartConnection target, PartConnection toLook)
        {
            var partTransform = transform;
            partTransform.rotation *= toLook.GetRotation(target.SideConnection);
            partTransform.position += target.transform.position - toLook.transform.position;
        }

        public void SetConnected(bool state)
        {
            _isConnected = state;
        }

    }
}