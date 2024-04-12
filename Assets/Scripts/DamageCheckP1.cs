using System.Collections;
using System.Collections.Generic;
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


    private void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision detected");
        if (player1.isPunching == true && collision.gameObject.tag == "Player2" && hasHit == false)
        {
            
            //game control
            if (player2.isBlocking == false)
            {
                audioSource.PlayOneShot(punchClip, 0.2f);
                player2.player2Health -= 10;
                p2Particles.Play();
                hasHit = true;
                Debug.Log(player2.player2Health);
                if (player2.player2Health <= 0)
                {
                    Destroy(player2.gameObject);
                    GameControl.victoryText = "Player 1 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }
                player1.isPunching = false;
            }
            if (player2.isBlocking == true)
            {
                audioSource.PlayOneShot(blockedpunchClip, 0.2f);
                player2.player2Health -= 0.5f;
                p2Particles.Play();
                hasHit = true;
                Debug.Log(player2.player2Health);
                if (player2.player2Health <= 0)
                {
                    Destroy(player2.gameObject);
                    GameControl.victoryText = "Player 1 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }
                
                player1.isPunching=false;
            }
            player1.isPunching = false;
            return;
        }
    }
}

