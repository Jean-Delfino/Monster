using System.Collections.Generic;
using UnityEngine;

namespace MountableMonster
{
    public class MonsterBody : MonoBehaviour
    {
        [SerializeField] private PartConnection connection;

        public class BodyExtension
        {        
            public float MinX = float.MinValue;
            public float MaxX = float.MinValue;
            public float MinY = float.MaxValue;
            public float MaxY = float.MaxValue;
        }
        public void SetMainConnection(MonsterPart monsterPart)
        {
            connection.ConnectPart(monsterPart);
        }

        public BodyExtension FindBodyExtends()
        {
            BodyExtension bounds = new();
            Queue<MonsterPart> monsterParts = new();
            HashSet<MonsterPart> visitedMonsterPart = new();
            
            monsterParts.Enqueue(connection.ConnectedPart);
            
            while (monsterParts.Count > 0)
            {
                var part = monsterParts.Dequeue();
                visitedMonsterPart.Add(part);
                
                foreach (var con in part.Connections)
                {
                    var auxPart = con.ConnectedPart;
                    if (auxPart != null && !visitedMonsterPart.Contains(auxPart))
                    {
                        monsterParts.Enqueue(auxPart);
                    }
                }

                CheckVisualPartsBounds(part,ref bounds);
            }
                    
            return bounds;
        }

        private void CheckVisualPartsBounds(MonsterPart part, ref BodyExtension bounds)
        {
            foreach (var rend in part.VisualComponents)
            {
                VerifyBounds(rend.bounds, ref bounds);
            }
        }
        private void VerifyBounds(Bounds visualPartBounds, ref BodyExtension bounds)
        {
            bounds.MaxX = Mathf.Max(visualPartBounds.max.x, bounds.MaxX);
            bounds.MaxY = Mathf.Max(visualPartBounds.max.y, bounds.MaxY);
            bounds.MinY = Mathf.Max(visualPartBounds.min.y, bounds.MinY);
            bounds.MinX = Mathf.Max(visualPartBounds.min.x, bounds.MinX);
        }
    }
}