using Reuse.Utils;
using UnityEngine;

public class TestLookAt : MonoBehaviour
{
    public Transform basePoint; 
    public Transform trunkToStack;
    public Transform trunkPoint;
    void Update()
    {
        //print("STACK = " + trunkToStack.up + "BASE = " + basePoint.up);

        if (basePoint != null && trunkToStack != null)
        {

            trunkPoint.rotation = trunkToStack.rotation = UtilTransform.GetRotationLookingAtTransform(trunkToStack,  basePoint);

            enabled = false;
        }
    }
}