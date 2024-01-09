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
            // Obtém a direção "up" dos objetos look e rotate
            Vector3 upLook = lookTarget.up;
            Vector3 upRotate = lookObject.up;

            // Verifica se os vetores upLook e upRotate são diferentes antes de aplicar a rotação
            if (upLook != upRotate * -1)
            {
                // Calcula a rotação necessária para fazer upRotate apontar na direção oposta de upLook
                Quaternion rotation = Quaternion.FromToRotation(upRotate, -upLook);

                // Aplica a rotação ao objeto rotateObject
                rotateObject.rotation *= rotation;
            }
        }
    }
}