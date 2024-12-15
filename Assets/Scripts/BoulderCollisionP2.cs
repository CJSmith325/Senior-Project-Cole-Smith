using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoulderCollisionP2 : MonoBehaviour
{

    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;
    private float shakeTimer;
    private float shakeDur;
    private Player2Controller play2;
    private Animator play1Animator;
    private Player1Controller play1;
    public float growthRate = 0.1f;
    public float shakeDuration = 3f;
    public float shakeAmplitude = 1f;  // Intensity of the shake
    public float shakeFrequency = 1f;  // Speed of the shake
    private bool hasContacted = false;

    private DamageCheckP2 dmgp2;
    private ParticleSystem rockParticles;

    private void Start()
    {
        play1Animator = FindAnyObjectByType<Player1Controller>().GetComponent<Animator>();
        play2 = this.gameObject.GetComponent<Player2Controller>();
        CinemachineVirtualCamera virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>();
        Debug.Log(virtualCamera);
        cinemachinePerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Debug.Log(cinemachinePerlin);
        dmgp2 = GameObject.FindAnyObjectByType<DamageCheckP2>().GetComponent<DamageCheckP2>();
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            shakeDur += Time.deltaTime;
            cinemachinePerlin.m_AmplitudeGain += Time.deltaTime;
            cinemachinePerlin.m_FrequencyGain += Time.deltaTime;


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

    private IEnumerator WaitCoupleSeconds()
    {
        yield return new WaitForSecondsRealtime(3f);

        GameControl.victoryText = "Player 1 Wins!";
        SceneManager.LoadScene("GameOverScreen");
    }

    IEnumerator DecreaseTimeScale()
    {
        float startScale = 1f;
        float endScale = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += (1.5f * Time.unscaledDeltaTime);
            Time.timeScale = Mathf.Lerp(startScale, endScale, elapsedTime / shakeDuration);
            Debug.Log(Time.timeScale);
            yield return null;
        }

        Time.timeScale = endScale; // Ensure the final value is exactly 0
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" && hasContacted == false)
        {
            play1 = other.GetComponent<Player1Controller>();
            play1Animator.SetBool("isHit", true);

            Transform particleSystemTransform = other.transform.Find("p1RockParticleSystem");
            if (particleSystemTransform != null)
            {
                rockParticles = particleSystemTransform.GetComponent<ParticleSystem>();
                if (rockParticles != null)
                {
                    rockParticles.Play();
                    play1.playRockSound();
                }
                else
                {
                    Debug.LogWarning("ParticleSystem component missing on p1RockParticleSystem.");
                }
            }
            else
            {
                Debug.LogWarning("p1RockParticleSystem not found as a child of Player1.");
            }

            dmgp2.animBool = true;
            play1.player1Health -= 20;
            hasContacted = true;
            if (play1.player1Health <= 0)
            {
                //Destroy(player2.gameObject);


                TriggerShake();
                play1Animator.SetBool("isDead", true);
                StartCoroutine(DecreaseTimeScale());
                StartCoroutine(WaitCoupleSeconds());

                return;
            }
            Destroy(this.gameObject);
        }

        else if (other.tag == "Player2")
        {

        }

        else if (other.tag == "Ground" && hasContacted == false)
        {
            play1.playRockSound();
            Destroy(this.gameObject);
        }

        else
        {

        }
    }

}
