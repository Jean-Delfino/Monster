using UnityEngine;

namespace DungeonGeneration
{
    public class DungeonRoom : ScriptableObject
    {
        [Space] [Header("ROOM SPACE DEFINITION")] [Space]
        [SerializeField] private RoomShape roomShape;

        [SerializeField] private Vector3 roomSize = new Vector3(5, 0, 5);
    
        [SerializeField] private Vector3 roomDoorLimiters = new Vector3(1, 0, 1); 
        
        public Vector3 RoomSize => roomSize;
        public Vector3 RoomCenter => roomSize / 2;
        public Vector3 RoomDoorLimiters => roomDoorLimiters;

        private IDungeonRoomShape _shape;
        
        public void Setup()
        {
            switch (roomShape)
            {
                case RoomShape.Square:
                    _shape = new SquareShapedRoom();
                    break;
            }
        }
    }
}