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
            connection.ConnectPart(monsterPart);
            monsterPart.SimpleConnect(this, monsterPartConnection);
            monsterPart.SetConnectionFacingConnection(connection, monsterPartConnection);
        }
        public void SimpleConnect(MonsterPart monsterPart, PartConnection connection)
        {
            connection.SimpleConnect(monsterPart);
        }

        private void SetConnectionFacingConnection(PartConnection target, PartConnection toLook)
        {
            transform.rotation *= UtilTransform.GetRotationInverseTwoUpRotations(toLook.transform, target.transform);
        }
        
        public void SetConnected(bool state)
        {
            _isConnected = state;
        }

        public void SetRotation(Vector3 rotation)
        {
            transform.rotation = Quaternion.Euler(rotation);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}