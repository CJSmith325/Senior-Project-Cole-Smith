using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player1Health : MonoBehaviour
{
    public Slider healthBar;
    public Player1Controller playerHealth;
    private void Start()
    {
        healthBar.maxValue = playerHealth.player1MaxHealth;
        healthBar.value = playerHealth.player1Health;
    }
    private void Update()
    {
        healthBar.value = playerHealth.player1Health;
    }
}