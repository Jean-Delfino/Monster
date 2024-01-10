using UnityEngine;

namespace MountableMonster
{
    public class PartConnection : MonoBehaviour
    {
        [SerializeField] private Vector3 partRotation = Vector3.zero;
        
        private MonsterPart _connectedPart;

        public void ConnectPart(MonsterPart part)
        {
            _connectedPart = part;
            part.SetConnected(true);

        }
    }
}