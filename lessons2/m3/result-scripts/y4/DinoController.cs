using UnityEngine;

namespace Lessons.M3.Y4
{
    public class DinoController : MonoBehaviour
    {
        public float speed = 5f;
        public float jumpForce = 5f;
        public float rayDistance = 1.1f;

        private Rigidbody rb;
        private bool isGrounded;
        private GameController gameController;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            gameController = FindFirstObjectByType<GameController>();
        }

        private void Update()
        {
            if (gameController.isGameOver)
            {
                return;
            }

            transform.position += Vector3.right * speed * Time.deltaTime;

            Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red);
            isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance);

            bool jumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
            if (isGrounded && jumpPressed)
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

