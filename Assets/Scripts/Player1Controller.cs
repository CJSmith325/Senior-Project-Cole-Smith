using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Animations;


public class Player1Controller : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isPunching = false;
    public bool isCrossPunching = false;
    public float player1Health = 100f;
    public float player1MaxHealth = 100f;
    public float player1Attack = 100f;
    public float player1MaxAttack = 100f;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Vector3 Velocity;
    public Transform groundCheck;
    public Transform attackObject;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    private MeshRenderer hitboxMesh;
    public CharacterController charController;
    private Player2Controller otherChar;
    public GameObject boulderPrefab;
    private GameObject boulderHolder;
    public bool isBlocking = false;
    public Animator anim;
    public SphereCollider P1RightHand;
    public SphereCollider P1LeftHand;
    public DamageCheckP1[] dmgP1;
    public AudioSource audioSource;
    public AudioClip jumpClip;
    private Rigidbody rb;
    public bool scaleFlip;
    public AudioClip boulderWhoosh;
    public AudioClip jabWhoosh;
    public AudioClip crossWhoosh;

    public AudioClip boulderClip;

    public AudioSource footStepSource;
    private float jumpTimer;
    private Material[] furMaterial;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    

    //private Rigidbody boulderRB;
    //private Vector3 boulderVector = new Vector3();

    private float footstepCooldown = 0.833f; // Cooldown between footstep sounds
    private float footstepTimer = 0f;
    float reverseSpeed = -1.0f; // Define your reverse speed

    private void Start()
    {
        //boulderRB = boulderPrefab.GetComponent<Rigidbody>();
        player1Attack = 100f;
        //hitboxMesh = GameObject.Find("meshCubeP1Sas1").GetComponent<MeshRenderer>();
        charController = this.GetComponent<CharacterController>();
        otherChar = FindAnyObjectByType<Player2Controller>();
        rb = this.GetComponent<Rigidbody>();
        //Debug.Log(hitboxMesh.gameObject.name);
        anim.SetBool("isGrounded", true);
        skinnedMeshRenderer = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        furMaterial = skinnedMeshRenderer.materials;
        foreach (Material mat in furMaterial)
        {

            // Adjust the material color
            mat.color = new Color(Mathf.Clamp01(mat.color.r + 0.5f), mat.color.g, Mathf.Clamp01(mat.color.b - 0.5f), mat.color.a);

        }


        


    }

    void Update()
    {

        Vector3 scale = transform.localScale;

        footstepTimer -= Time.deltaTime;

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

        if (player1Health <= 0)
        {
            this.gameObject.GetComponent<Player1Controller>().enabled = false;
            otherChar.enabled = false;
        }

        Vector3 movement = new Vector3(moveSpeed, 0f, 0f);
        // movement
        if (Input.GetKey(KeyCode.A) && this.transform.position.x + 18.4f >= otherChar.transform.position.x)
        {
            charController.Move(-movement * Time.deltaTime);
            
            if (isPunching == false && isAttacking == false && footstepTimer <= 0f)
            {
                
                anim.SetBool("isIdling", false);
                anim.SetBool("isWalking", true);
                anim.SetFloat("speed", -1f);
                footStepSource.Play();
                footstepTimer = footstepCooldown;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (isPunching == false && isAttacking == false)
            {
                
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdling", true);
                anim.SetFloat("speed", 1f);
                footStepSource.Stop();
            }
                
        }

        if (Input.GetKey(KeyCode.D) && this.transform.position.x <= otherChar.transform.position.x + 18.4f)
        {
            charController.Move(movement * Time.deltaTime);

            if (isPunching == false && isAttacking == false && footstepTimer <= 0f)
            {
                anim.SetFloat("speed", 1f);
                anim.SetBool("isIdling", false);
                anim.SetBool("isWalking", true);
                footStepSource.Play();
                footstepTimer = footstepCooldown;
            }
                
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (isPunching == false && isAttacking == false)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdling", true);
                footStepSource.Stop();
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
                //Debug.Log("grounded");
                Velocity.y = -6f;
                anim.SetBool("isGrounded", true);
            }

            if (Input.GetKey(KeyCode.W))
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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                anim.SetBool("isJumpAttacking", true);
                jumpTimer = 1.5f;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("isJumpAttacking", true);
                jumpTimer = 1.5f;
            }
        }

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (jumpTimer <= 0)
        {
            anim.SetBool("isJumpAttacking", false);
        }

        // move and jump with given input
        //charController.Move(movement.normalized * moveSpeed * Time.deltaTime);
        charController.Move(Velocity * Time.deltaTime);

        //Vector3 attackRadius = new Vector3(0.4f, 0.4f);
        Vector3 attackCenter = attackObject.transform.position;


        // basic attack
        if (Input.GetKeyDown(KeyCode.Q) && isPunching == false)
        {
            isPunching = true;
            StartCoroutine(BasicPunch());
        }

        if (Input.GetKeyDown(KeyCode.E) && isPunching == false)
        {
            isPunching = true;
            StartCoroutine(CrossPunch());
        }
        
        

        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (player1Attack >= 100f && isAttacking == false)
            {
                isAttacking = true;
                StartCoroutine(SpecialAttack());
            }
                
        }

        // block
        if (Input.GetKey(KeyCode.S))
        {
            
            isBlocking = true;
            Debug.Log("Blocking");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            
            isBlocking = false;
            Debug.Log("Not Blocking");
        }

        transform.localScale = scale;
        
    }

    public void playRockSound()
    {
        footStepSource.PlayOneShot(boulderClip);
    }

    public IEnumerator BasicPunch()
    {
        
            
            //Vector3 attackRadius = new Vector3(0.4f, 0.4f);
            //Vector3 attackCenter = attackObject.transform.position;

            //Debug.Log("Mesh on");
            anim.SetBool("isPunching", true);
            anim.SetBool("isIdling", false);
            anim.SetBool("isWalking", false);
            audioSource.PlayOneShot(jabWhoosh, 1f);

            yield return new WaitForSeconds(0.6f);
            anim.SetBool("isPunching", false);
            isPunching = false;
            //flip flag of damage check script
            dmgP1[0].hasHit = false;
            dmgP1[1].hasHit = false;
            anim.SetBool("isIdling", true);
        
    }

    public IEnumerator CrossPunch()
    {
        
            //Vector3 attackRadius = new Vector3(0.4f, 0.4f);
            //Vector3 attackCenter = attackObject.transform.position;

            //Debug.Log("Mesh on");
            anim.SetBool("isCrossPunching", true);
            anim.SetBool("isIdling", false);
            anim.SetBool("isWalking", false);
            audioSource.PlayOneShot(crossWhoosh, 1f);


            yield return new WaitForSeconds(0.6f);
            anim.SetBool("isCrossPunching", false);
            isPunching = false;
            //flip flag of damage check script
            dmgP1[0].hasHit = false;
            dmgP1[1].hasHit = false;
            anim.SetBool("isIdling", true);
        
    }

    public IEnumerator SpecialAttack()
    {
        
            
            isAttacking = true;
            player1Attack = 0;
            // do special attack
            anim.SetBool("isPunching", false);
            anim.SetBool("isIdling", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(0.8f);
            Vector3 attackCenter = attackObject.transform.position;
            boulderHolder = Instantiate(boulderPrefab, attackCenter, Quaternion.identity);
            boulderHolder.AddComponent<BoulderCollisionP1>();
            boulderHolder.GetComponent<Rigidbody>().AddRelativeForce(300f, 400f, 0);
            audioSource.PlayOneShot(boulderWhoosh);
            // check bouldercollision for rest of code
            Debug.Log("Special ATTACK");
            
            
            anim.SetBool("isAttacking", false);
            isAttacking = false;
            anim.SetBool("isIdling", true);
        
        
    }

    public GameObject[] offensiveParts;

    public void ResetHit()
    {
        foreach (var dmg in dmgP1)
        {
            dmg.hasHit = true; // Reset hit flags when the animation resets
            Debug.Log("I Reset!");
        }
    }

    public void CastHit()
    {
        foreach (var dmg in dmgP1)
        {
            
                dmg.hasHit = false;
                Debug.Log("I Casted!");
            
        }
    }

}
