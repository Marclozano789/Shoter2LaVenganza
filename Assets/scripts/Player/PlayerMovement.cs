using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 10f;
    private float gravity = -9.81f;

    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;

    bool isGrounded;
    Vector3 velocity;
    public float jumpHeight = 3f;

    void Update()
    {
        float dt = Time.timeScale == 0 ? Time.unscaledDeltaTime : Time.deltaTime;

        // Suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Input
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Movimiento relativo a la cámara
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 move = forward * z + right * x;

        // Aplicar movimiento
        characterController.Move(move * speed * dt);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        // Gravedad
        velocity.y += gravity * dt;
        characterController.Move(velocity * dt);
    }
}
