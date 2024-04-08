using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCheckP2 : MonoBehaviour
{
    public bool hasHit = false;
    public Player1Controller player1;
    public Player2Controller player2;
    public AudioClip punchClip;

    public AudioClip blockedpunchClip;

    public AudioSource audioSource;

    private void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision detected");
        if (player2.isPunching == true && collision.gameObject.tag == "Player1" && hasHit == false)
        {
            
            //game control
            if (player1.isBlocking == false)
            {
                audioSource.PlayOneShot(punchClip, 0.2f);
                player1.player1Health -= 10;
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
            player2.isPunching = false;
            return;
        }
    }
}
