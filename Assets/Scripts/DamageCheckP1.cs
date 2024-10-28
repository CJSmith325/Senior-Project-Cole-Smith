using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCheckP1 : MonoBehaviour
{
    public bool hasHit = false;
    public Player1Controller player1;
    public Player2Controller player2;
    public AudioClip punchClip;

    

    public ParticleSystem p2Particles;
    
    public AudioClip blockedpunchClip;
    
    public AudioSource audioSource;

    public Animator play2Animator;
    private bool animBool;

    private float targetTime = 0.28f;
    private float punchTime = 0.9f;

    
    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;
    private float shakeTimer;

    public float shakeDuration = 8f;
    public float shakeAmplitude = 4f;  // Intensity of the shake
    public float shakeFrequency = 4f;  // Speed of the shake


    private void Start()
    {
        CinemachineVirtualCamera virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>();
        
        cinemachinePerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (player1.isPunching == true)
        {
            punchTime -= Time.deltaTime;
            
        }
        if (player1.isPunching == false && punchTime == 0)
        {
            punchTime = 0.9f;
        }

        if (animBool == true)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0)
            {
                animBool = false;
                play2Animator.SetBool("isHit", false);
                play2Animator.SetBool("isIdling", true);
                targetTime = 0.28f;
            }
        }
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                // Reset the shake values
                cinemachinePerlin.m_AmplitudeGain = 0f;
                cinemachinePerlin.m_FrequencyGain = 0f;
            }
        }
    }

    public void TriggerShake()
    {
        shakeTimer = shakeDuration;
        cinemachinePerlin.m_AmplitudeGain = shakeAmplitude;
        cinemachinePerlin.m_FrequencyGain = shakeFrequency;
    }

    private IEnumerator WaitCoupleSeconds()
    {
        yield return new WaitForSeconds(2f);
    }



    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision detected");
        if (player1.isPunching == true && other.gameObject.tag == "Player2" && hasHit == false && punchTime <= 0)
        {

            //game control
            if (player2.isBlocking == false)
            {
                audioSource.PlayOneShot(punchClip, 0.2f);
                player2.player2Health -= 10;
                play2Animator.SetBool("isHit", true);
                animBool = true;
                p2Particles.Play();
                hasHit = true;
                Debug.Log(player2.player2Health);
                if (player2.player2Health <= 0)
                {
                    //Destroy(player2.gameObject);

                    Time.timeScale = 0.5f;
                    TriggerShake();
                    WaitCoupleSeconds();

                    GameControl.victoryText = "Player 1 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }
                player1.isPunching = false;
            }
            if (player2.isBlocking == true)
            {
                audioSource.PlayOneShot(blockedpunchClip, 0.2f);
                player2.player2Health -= 0.5f;
                play2Animator.SetBool("isHit", true);
                animBool = true;
                p2Particles.Play();
                hasHit = true;
                Debug.Log(player2.player2Health);
                if (player2.player2Health <= 0)
                {

                    //Destroy(player2.gameObject);
                    Time.timeScale = 0.5f;
                    TriggerShake();
                    WaitCoupleSeconds();

                    GameControl.victoryText = "Player 1 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }

                player1.isPunching = false;
            }
            //player1.isPunching = false;
            return;
        }
    }
    

    

    //private void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Collision detected");
    //    if (player1.isPunching == true && other.gameObject.tag == "Player2" && hasHit == false && punchTime <= 0)
    //    {

    //        //game control
    //        if (player2.isBlocking == false)
    //        {
    //            audioSource.PlayOneShot(punchClip, 0.2f);
    //            player2.player2Health -= 10;
    //            play2Animator.SetBool("isHit", true);
    //            animBool = true;
    //            p2Particles.Play();
    //            hasHit = true;
    //            Debug.Log(player2.player2Health);
    //            if (player2.player2Health <= 0)
    //            {
    //                Destroy(player2.gameObject);
    //                GameControl.victoryText = "Player 1 Wins!";
    //                SceneManager.LoadScene("GameOverScreen");
    //            }
    //            player1.isPunching = false;
    //        }
    //        if (player2.isBlocking == true)
    //        {
    //            audioSource.PlayOneShot(blockedpunchClip, 0.2f);
    //            player2.player2Health -= 0.5f;
    //            play2Animator.SetBool("isHit", true);
    //            animBool = true;
    //            p2Particles.Play();
    //            hasHit = true;
    //            Debug.Log(player2.player2Health);
    //            if (player2.player2Health <= 0)
    //            {
    //                Destroy(player2.gameObject);
    //                GameControl.victoryText = "Player 1 Wins!";
    //                SceneManager.LoadScene("GameOverScreen");
    //            }

    //            player1.isPunching = false;
    //        }
    //        //player1.isPunching = false;
    //        return;
    //    }
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("Collision detected");
    //    if (player1.isPunching == true && collision.gameObject.tag == "Player2" && hasHit == false && punchTime <= 0)
    //    {

    //        //game control
    //        if (player2.isBlocking == false)
    //        {
    //            audioSource.PlayOneShot(punchClip, 0.2f);
    //            player2.player2Health -= 10;
    //            play2Animator.SetBool("isHit", true);
    //            animBool = true;
    //            p2Particles.Play();
    //            hasHit = true;
    //            Debug.Log(player2.player2Health);
    //            if (player2.player2Health <= 0)
    //            {
    //                Destroy(player2.gameObject);
    //                GameControl.victoryText = "Player 1 Wins!";
    //                SceneManager.LoadScene("GameOverScreen");
    //            }
    //            player1.isPunching = false;
    //        }
    //        if (player2.isBlocking == true)
    //        {
    //            audioSource.PlayOneShot(blockedpunchClip, 0.2f);
    //            player2.player2Health -= 0.5f;
    //            play2Animator.SetBool("isHit", true);
    //            animBool = true;
    //            p2Particles.Play();
    //            hasHit = true;
    //            Debug.Log(player2.player2Health);
    //            if (player2.player2Health <= 0)
    //            {
    //                Destroy(player2.gameObject);
    //                GameControl.victoryText = "Player 1 Wins!";
    //                SceneManager.LoadScene("GameOverScreen");
    //            }

    //            player1.isPunching = false;
    //        }
    //        //player1.isPunching = false;
    //        return;
    //    }
    //}
}

