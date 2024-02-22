using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static string victoryText = "";
    public float timeLeft = 0;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI playerOne;
    public TextMeshProUGUI playerTwo;
    private CharacterHolding charHolder;

    private void Start()
    {
        timeLeft = 300;
        charHolder = FindAnyObjectByType<CharacterHolding>();
        playerOne.text = charHolder.characterOne;
        playerTwo.text = charHolder.characterTwo;
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.Round(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}
