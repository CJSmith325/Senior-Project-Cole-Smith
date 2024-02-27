using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player2Health : MonoBehaviour
{
    public Slider healthBar;
    public Slider attackBar;
    public Player2Controller playerHealth;
    private void Start()
    {
        healthBar.maxValue = playerHealth.player2MaxHealth;
        healthBar.value = playerHealth.player2Health;
        attackBar.maxValue = playerHealth.player2MaxAttack;
        attackBar.value = playerHealth.player2Attack;
    }
    private void Update()
    {
        healthBar.value = playerHealth.player2Health;
        attackBar.value = playerHealth.player2Attack;

        if (playerHealth.player2Attack < playerHealth.player2MaxAttack)
        {
            playerHealth.player2Attack = playerHealth.player2Attack + 5 * Time.deltaTime;
        }
    }
}
