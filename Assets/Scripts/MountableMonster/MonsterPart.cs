using UnityEngine;

namespace MountableMonster
{
    public class MonsterPart : MonoBehaviour
    {
        [SerializeField] private PartConnection[] connections;

        private bool _isConnected = false;
        public void ConnectMonsterPartToConnection(int connectionIndex, MonsterPart monsterPart)
        {
            ConnectMonsterPartToConnection(connections[connectionIndex], monsterPart);
        }

        public void ConnectMonsterPartToConnection(PartConnection connection, MonsterPart monsterPart)
        {
            connection.ConnectPart(monsterPart);
            
            transform.position = connection.transform.position;
        }
        
        public void SetConnected(bool state)
        {
            _isConnected = state;
        }

        public void SetRotation(Vector3 rotation)
        {
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}