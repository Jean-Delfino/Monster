using UnityEngine;

namespace MountableMonster
{
    public class PartConnection : MonoBehaviour
    {
        [SerializeField] private Vector3 partRotation = Vector3.zero;

        private MonsterPart _connectedPart;

        public void ConnectPart(MonsterPart part)
        {
            part.SetConnected(true);
            part.SetRotation(partRotation);
        }
    }
}