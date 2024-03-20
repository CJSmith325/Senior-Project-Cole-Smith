using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCheckP2 : MonoBehaviour
{
    public Player1Controller player1;
    public Player2Controller player2;

    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        if (player2.isPunching == true && collision.gameObject.tag == "Player1")
        {
            
            //game control
            if (player1.isBlocking == false)
            {
                player1.player1Health -= 10;
                Debug.Log(player1.player1Health);
                if (player1.player1Health <= 0)
                {
                    Destroy(player1.gameObject);
                    GameControl.victoryText = "Player 1 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }
            }
            if (player1.isBlocking == true)
            {
                player1.player1Health -= 0.5f;
                Debug.Log(player1.player1Health);
                if (player1.player1Health <= 0)
                {
                    Destroy(player2.gameObject);
                    GameControl.victoryText = "Player 1 Wins!";
                    SceneManager.LoadScene("GameOverScreen");
                }


            }
            player2.isPunching = false;
            return;
        }
    }
}
