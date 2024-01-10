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
            
            monsterPart.SetConnectionFacingConnection(connection, monsterPartConnection);
        }
        private void SimpleConnect(MonsterPart monsterPart, PartConnection connection)
        {
            connection.ConnectPart(monsterPart);
        }

        private void SetConnectionFacingConnection(PartConnection target, PartConnection toLook)
        {
            var partTransform = transform;
            partTransform.rotation *= Quaternion.Euler(toLook.GetRotation(target.SideConnection));
            partTransform.position += target.transform.position - toLook.transform.position;
        }

        public void SetConnected(bool state)
        {
            _isConnected = state;
        }

        public PartConnection[] GetConnections()
        {
            return connections;
        }
        
    }
}