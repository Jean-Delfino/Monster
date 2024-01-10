using UnityEngine;

namespace Test_Scripts
{
    public class RotateScript : MonoBehaviour
    {
#if UNITY_EDITOR

        public Transform targetLook;
        public Transform look;
        public Transform rotate;

        void Update()
        {
            // Certifique-se de que os vetores Up e Forward de Look sejam opostos aos de TargetLook
            Quaternion rotation = Quaternion.FromToRotation(look.up, -targetLook.up) *
                                  Quaternion.FromToRotation(look.forward, -targetLook.forward);

            // Aplique a rotação no objeto Rotate (pai de Look ou Look)
            rotate.rotation = rotation * rotate.rotation;
        }

#endif
    }
}