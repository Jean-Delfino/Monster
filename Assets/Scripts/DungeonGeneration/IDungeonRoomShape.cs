using UnityEngine;

namespace DungeonGeneration
{
    public enum RoomShape
    {
        Square,
    }
    public interface IDungeonRoomShape
    {
        public Vector3 FindDoorPointingDirection(DungeonRoom room, Vector3 doorPosition);
        public Quaternion ConnectTwoDoor(Vector3 directionFirstDoor, Vector3 directionSecondDoor);

        public Vector3 FindRandomDoorPosition(DungeonRoom room, float z = 0f);
    }
}