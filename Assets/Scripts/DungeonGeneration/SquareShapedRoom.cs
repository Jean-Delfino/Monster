using System;
using Reuse.Utils;
using UnityEngine;

namespace DungeonGeneration
{
    public class SquareShapedRoom : IDungeonRoomShape
    {
        public Vector3 FindDoorPointingDirection(DungeonRoom room, Vector3 doorPosition)
        {
            var center = room.RoomCenter;

            Vector3 direction = doorPosition - center;

            if (Math.Abs(direction.x) < 1) direction.x = 0;
            if (Math.Abs(direction.z) < 1) direction.z = 0;

            return direction;
        }

        public Quaternion ConnectTwoDoor(Vector3 directionFirstDoor, Vector3 directionSecondDoor)
        {
            return Quaternion.FromToRotation(directionSecondDoor, directionFirstDoor);
        }

        public Vector3 FindRandomDoorPosition(DungeonRoom room, float z = 0f)
        {
            var roomSize = room.RoomSize;
            var possibleSize = roomSize - room.RoomDoorLimiters;
            var randomPosition = new Vector3((float) UtilRandom.RandomGenerator.NextDouble() * possibleSize.x, 
                0, 
                (float) UtilRandom.RandomGenerator.NextDouble() * possibleSize.z);

            var randomAxis = UtilRandom.RandomGenerator.Next(2);

            if (randomAxis == 0)
                randomPosition.x = UtilMathOperations.RoundToClosestLimit(randomPosition.x, 0, possibleSize.x);
            else 
                randomPosition.z = UtilMathOperations.RoundToClosestLimit(randomPosition.z, 0, possibleSize.z);

            return randomPosition;
        }
    }
}