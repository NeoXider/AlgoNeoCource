using UnityEngine;

namespace Lessons.M3.Y4
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float offset;

        private void LateUpdate()
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x + offset;
            transform.position = pos;
        }
    }
}

