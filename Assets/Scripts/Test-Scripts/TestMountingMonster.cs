using System;
using MountableMonster;
using UnityEngine;

namespace Test_Scripts
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

            monsterBodyClone.SetMainConnection(parts[0]);
            parts[0].transform.SetParent(monsterBodyClone.Connection.transform);
            
            for (int i = 1; i < parts.Length; i++)
            {
                var connectToPart = parts[spawnParts[i].connectToPartId];

                connectToPart.GetConnectedToMonsterPart(spawnParts[i].connectToConnectionId, parts[i], spawnParts[i].connectionId);
                
                parts[i].transform.SetParent(connectToPart.transform);
                parts[i].name += $"{i}";
            }
            
            monsterBodyClone.gameObject.SetActive(true);

            var player = monsterBodyClone.GetComponent<Player>();

            player.SetMonsterBody(monsterBodyClone);
            player.PrepareNavMesh();
        }
        
#endif
    }
}