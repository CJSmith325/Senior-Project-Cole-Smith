using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player1Health : MonoBehaviour
{
    public Slider healthBar;
    public Slider attackBar;
    public Player1Controller playerHealth;
    private void Start()
    {
        healthBar.maxValue = playerHealth.player1MaxHealth;
        healthBar.value = playerHealth.player1Health;
        attackBar.maxValue = playerHealth.player1MaxAttack;
        attackBar.value = playerHealth.player1Attack;
    }
    private void Update()
    {
        healthBar.value = playerHealth.player1Health;
        attackBar.value = playerHealth.player1Attack;

        if (playerHealth.player1Attack < playerHealth.player1MaxAttack)
        {
            playerHealth.player1Attack = playerHealth.player1Attack + 5 * Time.deltaTime;
        }
    }
}