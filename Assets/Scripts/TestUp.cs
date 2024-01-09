using UnityEngine;

public class TestUp : MonoBehaviour
{
    public Transform lookTarget;    // O objeto que você quer que seja olhado
    public Transform rotateObject;  // O objeto que será rotacionado
    public Transform lookObject;
    void Update()
    {
        RotateToOppositeUpDirection();
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
        }
    }
}