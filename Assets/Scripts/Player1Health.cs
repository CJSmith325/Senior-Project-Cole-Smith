using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Health : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;
    public HealthBar1 healthBar;
    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamagePlayer(10);
        }
    }
    public void DamagePlayer(int damage)
    {
        curHealth -= damage;
        healthBar.SetHealth(curHealth);
    }

}
