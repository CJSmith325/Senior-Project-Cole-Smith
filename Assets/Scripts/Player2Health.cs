using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player2Health : MonoBehaviour
{
    public Slider healthBar;
    public Player2Controller playerHealth;
    private void Start()
    {
        healthBar.maxValue = playerHealth.player2MaxHealth;
        healthBar.value = playerHealth.player2Health;
    }
    private void Update()
    {
        healthBar.value = playerHealth.player2Health;
    }
}
