using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Controller : MonoBehaviour
{
    public float player1Health = 100f;
    public float player1MaxHealth = 100f;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Vector3 Velocity;
    public Transform groundCheck;
    public Transform attackObject;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public MeshRenderer hitboxMesh;
    private CharacterController charController;
    private Player2Controller otherChar;
    public bool isBlocking = false;

    private void Start()
    {
        hitboxMesh = GameObject.Find("meshCubeP1").GetComponent<MeshRenderer>();
        charController = this.GetComponent<CharacterController>();
        otherChar = FindAnyObjectByType<Player2Controller>();
        Debug.Log(hitboxMesh.gameObject.name);
    }

    void Update()
    {
        Vector3 movement = new Vector3(moveSpeed, 0f, 0f);
        // movement
        if (Input.GetKey(KeyCode.A))
        {
            charController.Move(-movement * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            charController.Move(movement * Time.deltaTime);
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
        //charController.Move(movement.normalized * moveSpeed * Time.deltaTime);
        charController.Move(Velocity * Time.deltaTime);

        // attack

        Vector3 attackRadius = new Vector3(0.4f, 0.4f);
        Vector3 attackCenter = attackObject.transform.position;

        if (Input.GetKeyDown(KeyCode.W))
        {
            hitboxMesh.enabled = true;
            Debug.Log("Mesh on");
            if (Physics.CheckBox(attackCenter, attackRadius, this.transform.rotation, playerLayer))
            {
                if (otherChar.isBlocking == false)
                {
                    otherChar.player2Health -= 10;
                    Debug.Log(otherChar.player2Health);
                    if (otherChar.player2Health <= 0)
                    {
                        Destroy(otherChar.gameObject);
                        GameControl.victoryText = "Player 1 Wins!";
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }
                if (otherChar.isBlocking == true)
                {
                    otherChar.player2Health -= 0.5f;
                    Debug.Log(otherChar.player2Health);
                    if (otherChar.player2Health <= 0)
                    {
                        Destroy(otherChar.gameObject);
                        GameControl.victoryText = "Player 1 Wins!";
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }
            }
            Debug.Log("mesh off");
            hitboxMesh.enabled = false;
        }

        // block
        if (Input.GetKey(KeyCode.S))
        {
            hitboxMesh.enabled = true;
            isBlocking = true;
            Debug.Log("Blocking");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            hitboxMesh.enabled = false;
            isBlocking = false;
            Debug.Log("Not Blocking");
        }

    }
}
