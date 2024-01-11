using Reuse.CameraControl;
using Reuse.Utils;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    [SerializeField] private float additionalRotation = 0f;
    void Update()
    {
        if(CameraController.Instance == null || !CameraController.IsMainCameraReady) return;
        Vector3 mousePos = Input.mousePosition;

        float angle = (Mathf.Atan2(mousePos.y / 540 - 1, mousePos.x / 960 - 1) * Mathf.Rad2Deg) + additionalRotation;
        print("Angle = " + angle + " " + Input.mousePosition + " ");
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
    }
}