using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.Mathematics;
//using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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


    public bool animBool;

    private float targetTime = 0.28f;
    private float punchTime = 0.9f;

    
    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;
    private float shakeTimer;
    private float shakeDur;

    public float growthRate = 0.1f;
    public float shakeDuration = 3f;
    public float shakeAmplitude = 0.7f;  // Intensity of the shake
    public float shakeFrequency = 0.7f;  // Speed of the shake

    private float shakeAmp = 0.5f;  // Intensity of the shake
    private float shakeFreq = 0.5f;  // Speed of the shake
    private float hitShakeDur = 0.08f;
    private float hitShakeTimer;


    public RectTransform uiElement;  // The UI element to shake
    public float shakeAmount = 10f;  // How much the UI element will shake
    public float healthshakeDuration = 0.5f;  // How long the shake lasts
    private Vector3 UIPosition;

    private NewSceneLoader loader;


    private void Start()
    {
        loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();
        Debug.Log(loader);
        CinemachineVirtualCamera virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>();
        Debug.Log(virtualCamera);
        cinemachinePerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Debug.Log(cinemachinePerlin);
        
        UIPosition = uiElement.transform.localPosition;
    }

    private void Update()
    {
        //if (player1.isPunching == true)
        //{
        //    punchTime -= Time.deltaTime;

        //}
        //if (player1.isPunching == false && punchTime == 0)
        //{
        //    punchTime = 0.9f;
        //}


        if (player1.isPunching)
        {
            punchTime -= Time.deltaTime;
            if (punchTime <= 0)
            {
                punchTime = 0; // Prevent it from going negative
            }
        }
        else
        {
            punchTime = 0.3f; // Reset when punching ends
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

        //game over shake
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            shakeDur += Time.deltaTime;
            cinemachinePerlin.m_AmplitudeGain += Time.deltaTime * 0.5f;
            cinemachinePerlin.m_FrequencyGain += Time.deltaTime * 0.5f;
            

            if (shakeTimer <= 0f)
            {
                // Reset the shake values
                cinemachinePerlin.m_AmplitudeGain = 0f;
                cinemachinePerlin.m_FrequencyGain = 0f;
            }
        }

        // hit shake

        if (hitShakeTimer > 0)
        {
            hitShakeTimer -= Time.deltaTime;
            if (hitShakeTimer <= 0)
            {
                cinemachinePerlin.m_AmplitudeGain = 0f;
                cinemachinePerlin.m_FrequencyGain = 0f;
            }
        }
    }

    public void TriggerShake()
    {
        shakeTimer = shakeDuration;
        //cinemachinePerlin.m_AmplitudeGain = shakeAmplitude;
        //cinemachinePerlin.m_FrequencyGain = shakeFrequency;
    }

    public void TriggerHitShake()
    {
        cinemachinePerlin.m_AmplitudeGain = shakeAmp;
        cinemachinePerlin.m_FrequencyGain = shakeFreq;
        hitShakeTimer = hitShakeDur;
    }

    private void WaitCoupleSeconds()
    {
        
        
        GameControl.victoryText = "Player 1 Wins!";
        loader.TransitionToScene("GameOverScreen");
    }

    IEnumerator DecreaseTimeScale()
    {
        float startScale = 1f;
        float endScale = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += (1.5f*Time.unscaledDeltaTime);
            if (Time.timeScale >= 0.2f)
            {
                Time.timeScale = Mathf.Lerp(startScale, endScale, elapsedTime / shakeDuration);
            }
            //Time.timeScale = Mathf.Lerp(startScale, endScale, elapsedTime / shakeDuration);
            Debug.Log(Time.timeScale);
            yield return null;
        }

        Time.timeScale = endScale; // Ensure the final value is exactly 0
    }

    private IEnumerator HealthShakeCoroutine()
    {
        float elapsed = 0f;
        TriggerHitShake();
        while (elapsed < healthshakeDuration)
        {
            float x = Random.Range(-shakeAmount, shakeAmount);
            float y = Random.Range(-shakeAmount, shakeAmount);

            uiElement.localPosition = UIPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        uiElement.localPosition = UIPosition;  // Reset position after shaking
    }

    private void OnTriggerStay(Collider other)
    {
        // Detect collision only if not already hit and the punch time has passed
        if (player1.isPunching == true && other.CompareTag("Player2") && !hasHit && punchTime <= 0.0f)
        {
            if (player2.isBlocking == false)
            {
                StartCoroutine(HealthShakeCoroutine());
                audioSource.PlayOneShot(punchClip, 0.2f);
                player2.player2Health -= 10;
                play2Animator.SetBool("isHit", true);
                animBool = true;
                p2Particles.Play();
                hasHit = true; // Prevent multiple hits during the same animation
                Debug.Log(player2.player2Health);

                if (player2.player2Health <= 0)
                {
                    TriggerShake();
                    play2Animator.SetBool("isDead", true);
                    StartCoroutine(DecreaseTimeScale());
                    WaitCoupleSeconds();
                }
                //play2Animator.SetBool("isHit", false);
                player1.isPunching = false; // Reset punch flag
            }
            else if (player2.isBlocking == true)
            {
                StartCoroutine(HealthShakeCoroutine());
                audioSource.PlayOneShot(blockedpunchClip, 0.2f);
                player2.player2Health -= 0.5f;
                play2Animator.SetBool("isHit", true);
                animBool = true;
                p2Particles.Play();
                hasHit = true; // Prevent multiple hits during the same animation
                Debug.Log(player2.player2Health);

                if (player2.player2Health <= 0)
                {
                    TriggerShake();
                    play2Animator.SetBool("isDead", true);
                    StartCoroutine(DecreaseTimeScale());
                    WaitCoupleSeconds();
                }
                //play2Animator.SetBool("isHit", false);
                player1.isPunching = false; // Reset punch flag
            }
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

