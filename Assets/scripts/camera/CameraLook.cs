using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public float mouseSensitivity = 80f;
    public Transform playerBody;

    float xRotation = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.timeScale == 0 ? Time.unscaledDeltaTime : Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X")* mouseSensitivity * dt ;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * dt;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
