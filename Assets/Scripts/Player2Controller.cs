using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float player2health = 100f;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Vector3 Velocity;
    public Transform groundCheck;
    public Transform attackObject;
    public LayerMask groundLayer;
    private CharacterController charController;
    private Player1Controller otherChar;
    public bool isBlocking = false;


    private void Start()
    {
        charController = this.GetComponent<CharacterController>();
        otherChar = FindAnyObjectByType<Player1Controller>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(moveSpeed, 0f, 0f);
        // movement
        if (Input.GetKey(KeyCode.L))
        {
            charController.Move(movement * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.J))
        {
           charController.Move(-movement * Time.deltaTime);
        }

        // movement
        //float horizontalInput = Input.GetAxis("Horizontal_P2");
        //Vector3 movement = new Vector3(horizontalInput, 0f, 0f);

        // jumping
        if (charController.isGrounded)
        {
            if (Velocity.y < 0)
            {
                Velocity.y = -6f;
            }
            
            if (Input.GetKey(KeyCode.I))
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
        //charController.Move(movement.normalized * moveSpeed * Time.deltaTime);
        charController.Move(Velocity * Time.deltaTime);


        //attack
        Vector3 attackRadius = new Vector3(0.5f, 0.5f);
        Vector3 attackCenter = attackObject.transform.position;

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (Physics.CheckBox(attackCenter, attackRadius))
            {
                if (otherChar.isBlocking == false)
                {
                    otherChar.player1Health -= 10;
                    Debug.Log(otherChar.player1Health);
                    if (otherChar.player1Health <= 0)
                    {
                        Destroy(otherChar.gameObject);
                    }
                }
                if (otherChar.isBlocking ==true)
                {
                    otherChar.player1Health -= 0.5f;
                    Debug.Log(otherChar.player1Health);
                    if (otherChar.player1Health <= 0)
                    {
                        Destroy(otherChar.gameObject);
                    }
                }
            }
        }


        // block
        if (Input.GetKey(KeyCode.K))
        {
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            isBlocking = false;
        }
    }
}
