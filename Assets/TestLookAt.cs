using TreeEditor;
using UnityEngine;

public class TestLookAt : MonoBehaviour
{
    public Transform basePoint; // A base do tronco no chão
    public Transform trunkToStack; // O tronco a ser empilhado em cima do outro
    public Transform trunkPoint;
    void Update()
    {
        print("STACK = " + trunkToStack.up + "BASE = " + basePoint.up);

        if (basePoint != null && trunkToStack != null)
        {
            // Obtém a direção para frente dos troncos
            Vector3 forwardDirectionBase = basePoint.up;
            Vector3 forwardDirectionTrunk = trunkToStack.up;
        
            // Calcula a rotação necessária para alinhar os troncos
            Quaternion targetRotation = Quaternion.FromToRotation(forwardDirectionTrunk, forwardDirectionBase) * trunkToStack.rotation;
        
            // Aplica a rotação ao tronco a ser empilhado
            trunkToStack.rotation = targetRotation;
            trunkPoint.rotation = targetRotation;
        }
    }
}