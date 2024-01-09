using System;
using UnityEngine;

public class TestUp : MonoBehaviour
{
#if UNITY_EDITOR

    public Transform lookTarget;    // O objeto que você quer que seja olhado
    public Transform rotateObject;  // O objeto que será rotacionado
    public Transform lookObject;
    void Update()
    {
        RotateToOppositeUpDirection();
    }

    private void OnEnable()
    {
        print("UP (TARGET/LOOK) = " + lookTarget.up + lookObject.up);
    }

    void RotateToOppositeUpDirection()
    {
        if (lookTarget != null && rotateObject != null)
        {

            Vector3 upLook = lookTarget.up;
            Vector3 upRotate = lookObject.up;
            
            upLook.Normalize();
            upRotate.Normalize();
            
            if (upLook != upRotate * -1)
            {
                Quaternion rotation = Quaternion.FromToRotation(upRotate, -upLook);

                // Aplica a rotação ao objeto rotateObject
                rotateObject.rotation *= rotation;
            }
            
            print("NAMES = (TARGET/LOOK)" + lookTarget.name + " " + lookObject.name);

        }
    }
#endif
}