using UnityEngine;

namespace Test_Scripts
{
    public class PrintAxis : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Update()
        {
            var transform1 = transform;
            print(name + ": (UP, FORWARD, RIGHT)" + transform1.up + " " + transform1.forward + " " + transform1.right);
        } 
    
#endif
    
    }
}