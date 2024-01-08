using UnityEngine;

namespace MountableMonster
{
    public class MonsterPart : MonoBehaviour
    {
        [SerializeField] private PartConnection[] connections;

        private bool _isConnected = false;
        
        public void SimpleConnect(MonsterPart monsterPart, PartConnection connection)
        {
            connection.SimpleConnect(monsterPart);
        }
        public void ConnectMonsterPartToConnection(int connectionIndex, MonsterPart monsterPart, int monsterPartConnection)
        {
            ConnectMonsterPartToConnection(connections[connectionIndex], monsterPart, monsterPart.connections[monsterPartConnection]);
        }

        public void ConnectMonsterPartToConnection(PartConnection connection, MonsterPart monsterPart, PartConnection monsterPartConnection)
        {
            connection.ConnectPart(monsterPart);
            monsterPart.SimpleConnect(this, monsterPartConnection);
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