using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DamageCheckP2 : MonoBehaviour
{
    public bool hasHit = false;
    public Player1Controller player1;
    public Player2Controller player2;
    public AudioClip punchClip;

    public AudioClip blockedpunchClip;

    public ParticleSystem p1Particles;

    public AudioSource audioSource;

    public Animator play1Animator;


    private bool animBool;

    private float targetTime = 0.28f;
    private float punchTime = 0.9f;

    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;
    private float shakeTimer;
    private float shakeDur;

    public float growthRate = 0.1f;
    public float shakeDuration = 3f;
    public float shakeAmplitude = 0.5f;  // Intensity of the shake
    public float shakeFrequency = 0.5f;  // Speed of the shake

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
        if (player2.isPunching == true)
        {
            punchTime -= Time.deltaTime;

        }
        if (player2.isPunching == false && punchTime == 0)
        {
            punchTime = 0.9f;
        }

        if (animBool == true)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0)
            {
                animBool = false;
                play1Animator.SetBool("isHit", false);
                play1Animator.SetBool("isIdling", true);
                targetTime = 0.28f;
            }
        }


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
    }

    public void TriggerShake()
    {
        shakeTimer = shakeDuration;
        //cinemachinePerlin.m_AmplitudeGain = shakeAmplitude;
        //cinemachinePerlin.m_FrequencyGain = shakeFrequency;

        
    }

    private void WaitCoupleSeconds()
    {
        
        
        GameControl.victoryText = "Player 2 Wins!";
        loader.TransitionToScene("GameOverScreen");
    }

    IEnumerator DecreaseTimeScale()
    {
        float startScale = 1f;
        float endScale = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += (1.5f * Time.unscaledDeltaTime);
            if (Time.timeScale >= 0.2f)
            {
                Time.timeScale = Mathf.Lerp(startScale, endScale, elapsedTime / shakeDuration);
            }
            //Time.timeScale = Mathf.Lerp(startScale, endScale, elapsedTime / shakeDuration);
            Debug.Log(Time.timeScale);
            yield return null;
        }

        //Time.timeScale = endScale; // Ensure the final value is exactly 0
    }

    private IEnumerator HealthShakeCoroutine()
    {
        float elapsed = 0f;

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
        Debug.Log("Collision detected");
        if (player2.isPunching == true && other.gameObject.tag == "Player1" && hasHit == false && punchTime <= 0)
        {

            //game control
            if (player1.isBlocking == false && hasHit == false)
            {
                audioSource.PlayOneShot(punchClip, 0.2f);
                player1.player1Health -= 10;
                play1Animator.SetBool("isHit", true);
                animBool = true;
                p1Particles.Play();
                hasHit = true;
                Debug.Log(player1.player1Health);
                if (player1.player1Health <= 0)
                {
                    
                    TriggerShake();
                    play1Animator.SetBool("isDead", true );
                    StartCoroutine(DecreaseTimeScale());
                    WaitCoupleSeconds();
                }
                player2.isPunching = false;
            }
            if (player1.isBlocking == true && hasHit == false)
            {
                audioSource.PlayOneShot(blockedpunchClip, 0.2f);
                player1.player1Health -= 0.5f;
                play1Animator.SetBool("isHit", true);
                animBool = true;
                p1Particles.Play();
                hasHit = true;
                Debug.Log(player1.player1Health);
                if (player1.player1Health <= 0)
                {
                    
                    TriggerShake();
                    play1Animator.SetBool("isDead", true);
                    StartCoroutine(DecreaseTimeScale());
                    WaitCoupleSeconds();
                }

                player2.isPunching = false;
            }
            //player2.isPunching = false;
            return;
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("Collision detected");
    //    if (player2.isPunching == true && collision.gameObject.tag == "Player1" && hasHit == false && punchTime <= 0)
    //    {
            
    //        //game control
    //        if (player1.isBlocking == false)
    //        {
    //            audioSource.PlayOneShot(punchClip, 0.2f);
    //            player1.player1Health -= 10;
    //            play1Animator.SetBool("isHit", true);
    //            animBool = true;
    //            p1Particles.Play();
    //            hasHit = true;
    //            Debug.Log(player1.player1Health);
    //            if (player1.player1Health <= 0)
    //            {
    //                Destroy(player1.gameObject);
    //                GameControl.victoryText = "Player 2 Wins!";
    //                SceneManager.LoadScene("GameOverScreen");
    //            }
    //            player2.isPunching = false;
    //        }
    //        if (player1.isBlocking == true)
    //        {
    //            audioSource.PlayOneShot(blockedpunchClip, 0.2f);
    //            player1.player1Health -= 0.5f;
    //            play1Animator.SetBool("isHit", true);
    //            animBool = true;
    //            p1Particles.Play();
    //            hasHit = true;
    //            Debug.Log(player1.player1Health);
    //            if (player1.player1Health <= 0)
    //            {
    //                Destroy(player2.gameObject);
    //                GameControl.victoryText = "Player 2 Wins!";
    //                SceneManager.LoadScene("GameOverScreen");
    //            }

    //            player2.isPunching = false;
    //        }
    //        //player2.isPunching = false;
    //        return;
    //    }
    //}
}
