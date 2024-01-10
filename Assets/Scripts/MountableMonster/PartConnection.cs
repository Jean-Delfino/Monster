using System;
using System.Linq;
using UnityEngine;

namespace MountableMonster
{
    public enum SideConnection
    {
        None = -1,
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

        private MonsterPart _connectedPart;
        public MonsterPart ConnectedPart => _connectedPart;

        public SideConnection SideConnection => sideConnection;
        
        public void ConnectPart(MonsterPart part)
        {
            _connectedPart = part;
            part.SetConnected(true);
        }

        public Quaternion GetRotation(SideConnection connectionType)
        {
            return connectionType!= SideConnection.None ? 
                Quaternion.Euler(sideConnectionsRotation.FirstOrDefault(e => e.connection == connectionType)!.rotation) :
                Quaternion.identity;
        }

    }
}