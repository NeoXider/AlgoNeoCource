using UnityEngine;

namespace Lessons.M3.Y3
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        private void LateUpdate()
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x + offset.x;
            transform.position = pos;
        }
    }
}

