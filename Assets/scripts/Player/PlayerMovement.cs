using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 10f;
    private float gravity = -9.81f;

    Vector3 velocity;
    
    void Update()
    {
       
        float x= Input.GetAxis( "Horizontal" );
        float z = Input.GetAxis("Vertical");

        Vector3 move= transform.right * x + transform.forward * z;

        characterController.Move( move * speed * Time.deltaTime );
        
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);







    }
}
