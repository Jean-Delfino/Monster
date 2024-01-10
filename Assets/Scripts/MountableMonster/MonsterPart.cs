using Reuse.Utils;
using UnityEngine;

namespace MountableMonster
{
    public class MonsterPart : MonoBehaviour
    {
        [SerializeField] private PartConnection[] connections;

        private bool _isConnected = false;
        public void GetConnectedToMonsterPart(int connectionIndex, MonsterPart monsterPart, int monsterPartConnection)
        {
            GetConnectedToMonsterPart(connections[connectionIndex], monsterPart, monsterPart.connections[monsterPartConnection]);
        }

        public void GetConnectedToMonsterPart(PartConnection connection, MonsterPart monsterPart, PartConnection monsterPartConnection)
        {
            SimpleConnect(monsterPart, connection); //This getting connected with monster part
            SimpleConnect(this, monsterPartConnection); //Monster part getting connected with this
            
            monsterPart.SetConnectionFacingConnection(connection.transform, monsterPartConnection.transform);
        }
        private void SimpleConnect(MonsterPart monsterPart, PartConnection connection)
        {
            connection.ConnectPart(monsterPart);
        }

        private void SetConnectionFacingConnection(Transform target, Transform toLook)
        {
            var partTransform = transform;
            partTransform.rotation *= UtilTransform.GetRotationInverseTwoUpRotations(toLook, target);
            partTransform.position += target.position - toLook.position;
        }

        public void SetConnected(bool state)
        {
            _isConnected = state;
        }
        
    }
}