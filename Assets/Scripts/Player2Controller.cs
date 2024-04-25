using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class Player2Controller : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isPunching = false;
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
    public CharacterController charController;
    private Player1Controller otherChar;
    public bool isBlocking = false;
    private MeshRenderer hitboxMesh;
    public GameObject boulderPrefab;
    private GameObject boulderHolder;
    public Animator anim;
    public DamageCheckP2[] dmgP2;
    public AudioSource audioSource;
    public AudioClip jumpClip;
    public bool scaleFlip;


    private void Start()
    {
        //hitboxMesh = GameObject.Find("meshCubeP2").GetComponent<MeshRenderer>();
        
        otherChar = FindAnyObjectByType<Player1Controller>();
    }

    void Update()
    {

        Vector3 scale = transform.localScale;

        if (otherChar.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (scaleFlip ? -1 : 1);
            scale.z = Mathf.Abs(scale.z) * -1 * (scaleFlip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (scaleFlip ? -1 : 1);
            scale.z = Mathf.Abs(scale.z) * (scaleFlip ? -1 : 1);
        }

        if (player2Health <= 0)
        {
            Destroy(this.gameObject);
            GameControl.victoryText = "Player 1 Wins!";
            SceneManager.LoadScene("GameOverScreen");
        }

        Vector3 movement = new Vector3(moveSpeed, 0f, 0f);
        // movement
        if (Input.GetKey(KeyCode.L))
        {
            charController.Move(movement * Time.deltaTime);
            if (isPunching == false && isAttacking == false)
            {
                anim.SetBool("isIdling", false);
                anim.SetBool("isWalking", true);
            }
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (isPunching == false && isAttacking == false)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdling", true);
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
           charController.Move(-movement * Time.deltaTime);

           if (isPunching == false && isAttacking == false)
           {
                anim.SetBool("isIdling", false);
                anim.SetBool("isWalking", true);
           }
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            if (isPunching == false && isAttacking == false)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdling", true);
            }
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
                anim.SetBool("isGrounded", true);
            }
            
            if (Input.GetKey(KeyCode.I))
            {
                audioSource.PlayOneShot(jumpClip, 0.2f);
                Velocity.y = jumpForce;
                anim.SetBool("isGrounded", false);
            }

        }
        else
        {
            if (Velocity.y < 0)
            {
                anim.SetBool("isFalling", true);
            }
            if (Velocity.y > -9.81f)
            {
                Velocity.y -= 19.62f * Time.deltaTime;
                
            }
            
        }
        // move and jump with given input
        //charController.Move(movement.normalized * moveSpeed * Time.deltaTime);
        charController.Move(Velocity * Time.deltaTime);


        //attack
        
        Vector3 attackCenter = attackObject.transform.position;

        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(BasicPunch());
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            StartCoroutine(CrossPunch());
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(SpecialAttack());
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

        transform.localScale = scale;
    }

    public IEnumerator BasicPunch()
    {
        if (isPunching == false)
        {
            isPunching = true;
            //Vector3 attackRadius = new Vector3(0.4f, 0.4f);
            //Vector3 attackCenter = attackObject.transform.position;

            //Debug.Log("Mesh on");
            anim.SetBool("isPunching", true);
            anim.SetBool("isIdling", false);
            anim.SetBool("isWalking", false);

            yield return new WaitForSeconds(0.5f);
            anim.SetBool("isPunching", false);
            isPunching = false;
            //flip flag of damage check script
            dmgP2[0].hasHit = false;
            dmgP2[1].hasHit = false;
            anim.SetBool("isIdling", true);
        }
    }

    public IEnumerator CrossPunch()
    {
        if (isPunching == false)
        {
            isPunching = true;
            //Vector3 attackRadius = new Vector3(0.4f, 0.4f);
            //Vector3 attackCenter = attackObject.transform.position;

            //Debug.Log("Mesh on");
            anim.SetBool("isCrossPunching", true);
            anim.SetBool("isIdling", false);
            anim.SetBool("isWalking", false);



            yield return new WaitForSeconds(0.5f);
            anim.SetBool("isCrossPunching", false);
            isPunching = false;
            //flip flag of damage check script
            dmgP2[0].hasHit = false;
            dmgP2[1].hasHit = false;
            anim.SetBool("isIdling", true);
        }
    }

    public IEnumerator SpecialAttack()
    {
        if (player2Attack >= 100f)
        {

            isAttacking = true;
            // do special attack
            anim.SetBool("isPunching", false);
            anim.SetBool("isIdling", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(2.3f);
            Vector3 attackCenter = attackObject.transform.position;
            boulderHolder = Instantiate(boulderPrefab, attackCenter, Quaternion.identity);
            boulderHolder.AddComponent<BoulderCollisionP2>();
            boulderHolder.GetComponent<Rigidbody>().AddRelativeForce(-300f, 400f, 0);
            // check bouldercollision for rest of code
            Debug.Log("Special ATTACK");
            player2Attack = 0;

            anim.SetBool("isAttacking", false);
            isAttacking = false;
            anim.SetBool("isIdling", true);
        }

    }
}
