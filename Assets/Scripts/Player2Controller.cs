using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Controller : MonoBehaviour
{
    public float player2Health = 100f;
    public float player2MaxHealth = 100f;
    public float player2Attack = 100f;
    public float player2MaxAttack = 100f;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Vector3 Velocity;
    public Transform groundCheck;
    public Transform attackObject;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    private CharacterController charController;
    private Player1Controller otherChar;
    public bool isBlocking = false;
    private MeshRenderer hitboxMesh;
    public GameObject boulderPrefab;
    private GameObject boulderHolder;


    private void Start()
    {
        hitboxMesh = GameObject.Find("meshCubeP2").GetComponent<MeshRenderer>();
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
        Vector3 attackRadius = new Vector3(0.4f, 0.4f);
        Vector3 attackCenter = attackObject.transform.position;

        if (Input.GetKeyDown(KeyCode.O))
        {
            hitboxMesh.enabled = true;
            if (Physics.CheckBox(attackCenter, attackRadius, this.transform.rotation, playerLayer))
            {
                if (otherChar.isBlocking == false)
                {
                    otherChar.player1Health -= 10;
                    Debug.Log(otherChar.player1Health);
                    if (otherChar.player1Health <= 0)
                    {
                        Destroy(otherChar.gameObject);
                        GameControl.victoryText = "Player 2 Wins!";
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }
                if (otherChar.isBlocking ==true)
                {
                    otherChar.player1Health -= 0.5f;
                    Debug.Log(otherChar.player1Health);
                    if (otherChar.player1Health <= 0)
                    {
                        Destroy(otherChar.gameObject);
                        GameControl.victoryText = "Player 2 Wins!";
                        SceneManager.LoadScene("GameOverScreen");
                    }
                }
            }
            hitboxMesh.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (player2Attack >= 100f)
            {
                // do special attack
                boulderHolder = Instantiate(boulderPrefab, attackCenter, Quaternion.identity);
                boulderHolder.AddComponent<BoulderCollisionP2>();
                boulderHolder.GetComponent<Rigidbody>().AddRelativeForce(-150f, 200f, 0);
                // check bouldercollision for rest of code
                Debug.Log("Special ATTACK");
                player2Attack = 0;
            }
        }


        // block
        if (Input.GetKey(KeyCode.K))
        {
            hitboxMesh.enabled = true;
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            hitboxMesh.enabled = false;
            isBlocking = false;
        }
    }
}
