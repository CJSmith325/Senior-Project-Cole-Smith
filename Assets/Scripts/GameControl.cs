using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static string victoryText = "";
    public float timeLeft = 0;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI playerOne;
    public TextMeshProUGUI playerTwo;
    private CharacterHolding charHolder;
    public GameObject player1;
    public GameObject player2;
    private Color color1;
    private Color color2;

    private NewSceneLoader loader;

    private void Start()
    {
        loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();
        timeLeft = 120;
        charHolder = FindAnyObjectByType<CharacterHolding>();
        playerOne.text = charHolder.characterOne;
        playerTwo.text = charHolder.characterTwo;
        color1 = Color.white;
        color2 = Color.red;
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.Round(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            victoryText = "Time Expired!";
            loader.TransitionToScene("GameOverScreen");
        }

        if (timeLeft <= 30)
        {
            FlashingText();
        }

    }

    public void FlashingText()
    {
        timerText.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1f));
    }
}
