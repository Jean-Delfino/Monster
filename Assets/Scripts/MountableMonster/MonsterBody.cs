using UnityEngine;

namespace MountableMonster
{
    public class MonsterBody : MonoBehaviour
    {
        [SerializeField] private PartConnection connection;

        public void SetMainConnection(MonsterPart monsterPart)
        {
            connection.ConnectPart(monsterPart);
        }
    }
}