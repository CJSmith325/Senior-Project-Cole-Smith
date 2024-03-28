using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCheckP1 : MonoBehaviour
{
    public bool hasHit = false;
    public Player1Controller player1;
    public Player2Controller player2;

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        if (player1.isPunching == true && collision.gameObject.tag == "Player2" && hasHit == false)
        {
            
            //game control
            if (player2.isBlocking == false)
            {
                player2.player2Health -= 10;
                hasHit = true;
                Debug.Log(player2.player2Health);
                if (player2.player2Health <= 0)
                {
                    Destroy(player2.gameObject);
                    GameControl.victoryText = "Player 1 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }
            }
            if (player2.isBlocking == true)
            {
                player2.player2Health -= 0.5f;
                hasHit = true;
                Debug.Log(player2.player2Health);
                if (player2.player2Health <= 0)
                {
                    Destroy(player2.gameObject);
                    GameControl.victoryText = "Player 1 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }
                

            }
            player1.isPunching = false;
            return;
        }
    }
}

