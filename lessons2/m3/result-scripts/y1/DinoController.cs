using UnityEngine;

namespace Lessons.M3.Y1
{
    public class DinoController : MonoBehaviour
    {
        public float speed = 5f;
        public float jumpForce = 5f;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void Jump()
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }
    }
}

