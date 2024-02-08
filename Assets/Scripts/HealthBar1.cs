using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar1 : MonoBehaviour
{
    public Slider healthBar;
    public Player1Health playerHealth;
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1Health>();
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
    }
    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }

}
