using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        if (player2.isPunching == true)
        {
            punchTime -= Time.deltaTime;

        }
        if (player2.isPunching == false && punchTime <= 0)
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
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Collision detected");
        if (player2.isPunching == true && other.gameObject.tag == "Player1" && hasHit == false && punchTime <= 0)
        {

            //game control
            if (player1.isBlocking == false)
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
                    Destroy(player1.gameObject);
                    GameControl.victoryText = "Player 2 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }
                player2.isPunching = false;
            }
            if (player1.isBlocking == true)
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
                    Destroy(player2.gameObject);
                    GameControl.victoryText = "Player 2 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
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
