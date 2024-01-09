using Reuse.Utils;
using UnityEngine;

public class TestLookAt : MonoBehaviour
{
    
#if UNITY_EDITOR
    public Transform basePoint; 
    public Transform trunkToStack;
    public Transform trunkPoint;
    void Update()
    {
        //print("STACK = " + trunkToStack.up + "BASE = " + basePoint.up);

        if (basePoint != null && trunkToStack != null)
        {

            trunkPoint.rotation = trunkToStack.rotation = UtilTransform.GetRotationLookingTwoUpRotations(trunkToStack,  basePoint);

            enabled = false;
        }
    }
#endif
    
}