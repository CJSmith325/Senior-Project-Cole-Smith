using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Vector3 Velocity;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private CharacterController charController;

    private void Start()
    {
        charController = this.GetComponent<CharacterController>();
    }

    void Update()
    {

        // movement
        float horizontalInput = Input.GetAxis("Horizontal_P1");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f);
        

        // jumping
        if (charController.isGrounded)
        {
            if (Velocity.y < 0)
            {
                Velocity.y = -25f;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Velocity.y = jumpForce;
            }

        }
        else
        {
            if (Velocity.y > -9.81f)
            {
                Velocity.y -= 9.81f * Time.deltaTime;
            }
        }
        // move and jump with given input
        charController.Move(movement.normalized * moveSpeed * Time.deltaTime);
        charController.Move(Velocity * Time.deltaTime);

        // reset input
        horizontalInput = 0;
    }
}
