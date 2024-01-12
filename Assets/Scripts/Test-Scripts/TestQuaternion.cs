using UnityEngine;

namespace Test_Scripts
{
    public class TestQuaternion : MonoBehaviour
    {
#if UNITY_EDITOR
        public Vector3 fromQuaternion;
        public Vector3 toQuaternion;

        public Vector3 addEuler = new Vector3(0, 90, 0);
        private void Update()
        {
            var fromTo = Quaternion.FromToRotation(fromQuaternion, -toQuaternion);
            transform.rotation = (fromTo * Quaternion.Euler(addEuler));
        }
    }
#endif

}
