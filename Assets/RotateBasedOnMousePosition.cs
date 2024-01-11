using UnityEngine;

public class RotateBasedOnMousePosition : MonoBehaviour
{
    [SerializeField] private float additionalRotation = 0f;
    [SerializeField] private float rotationEndMultiplier = -1f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    protected Vector3 ActualMousePos;
    protected float ActualAngle;
    void Update()
    {
        ActualMousePos = Input.mousePosition;

        ActualAngle = (Mathf.Atan2((ActualMousePos.y / ((float)Screen.width / 2)) /  - 1, 
            (ActualMousePos.x / ((float)Screen.height / 2)) - 1) * Mathf.Rad2Deg) + additionalRotation;
        
        transform.rotation = Quaternion.AngleAxis(rotationEndMultiplier * ActualAngle, rotationAxis);
    }
}