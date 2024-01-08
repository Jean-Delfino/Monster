using System;
using UnityEngine;

namespace MountableMonster
{
    public class TestMountingMonster : MonoBehaviour
    {
#if UNITY_EDITOR
        //Prefabs
        [SerializeField] private MonsterBody monsterBody;
        
        [Serializable]
        public class SpawnPart
        {
            public MonsterPart part;
            public int connectToPartId;
            public int connectToConnectionId;

            public int connectionId;
        }

        [SerializeField] private SpawnPart[] spawnParts;
        private void Start()
        {
            CombineMonster();
        }

        private void CombineMonster()
        {
            var monsterBodyClone = Instantiate(monsterBody) as MonsterBody;

            MonsterPart[] parts = new MonsterPart[spawnParts.Length];

            for (int i = 0; i < spawnParts.Length; i++)
            {
                parts[i] = Instantiate(spawnParts[i].part, monsterBodyClone.transform) as MonsterPart;
            }

            monsterBody.SetMainConnection(parts[0]);

            for (int i = 1; i < parts.Length; i++)
            {
                var connectToPart = parts[spawnParts[i].connectToPartId];
                parts[i].transform.SetParent(connectToPart.transform);

                connectToPart.ConnectMonsterPartToConnection(spawnParts[i].connectToConnectionId, parts[i], spawnParts[i].connectionId);
            }
        }
        
#endif
    }
}