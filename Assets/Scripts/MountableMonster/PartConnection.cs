using System;
using System.Linq;
using UnityEngine;

namespace MountableMonster
{
    public enum SideConnection
    {
        Top = 0,
        Bottom = 1,
        Right = 2,
        Left = 3,
        Front = 4,
        Back = 5,
    }
    public class PartConnection : MonoBehaviour
    {
        [SerializeField] private Vector3 partRotation = Vector3.zero;
       
        [Serializable]
        public class SideRotationRotation
        {
#if UNITY_EDITOR
            public string name;
#endif
            public SideConnection connection;
            public Vector3 rotation;
        }
        
        [SerializeField] private SideRotationRotation[] sideConnectionsRotation;
        [SerializeField] private SideConnection sideConnection;

        public SideConnection SideConnection => sideConnection;
        
        private MonsterPart _connectedPart;
        
        public void ConnectPart(MonsterPart part)
        {
            _connectedPart = part;
            part.SetConnected(true);
        }

        public Vector3 GetRotation(SideConnection connectionType)
        {
            return sideConnectionsRotation.FirstOrDefault(e => e.connection == connectionType)!.rotation;
        }

        public MonsterPart GetConnectedPart()
        {
            return _connectedPart;
        }
    }
}