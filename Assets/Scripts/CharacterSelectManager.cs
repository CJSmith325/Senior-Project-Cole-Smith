using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelectManager : MonoBehaviour
{
    private int selectCount;
    public CharacterHolding stringHold;
    private EnvironmentHolding environmentHolder;
    public TextMeshProUGUI playerText; 
    // Start is called before the first frame update
    void Start()
    {
        environmentHolder = GameObject.FindAnyObjectByType<EnvironmentHolding>();
        selectCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectCount == 2)
        {
            SceneManager.LoadScene(environmentHolder.environmentName);
        }
    }

    public void AssignSasquatch()
    {
        if (selectCount == 0)
        {
            stringHold.characterOne = "Sasquatch";
            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {
            stringHold.characterTwo = "Sasquatch";
            selectCount++;
        }
    }

    public void AssignMothman()
    {
        if (selectCount == 0)
        {
            stringHold.characterOne = "Mothman";
            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {
            stringHold.characterTwo = "Mothman";
            selectCount++;
        }
    }

    public void AssignDragon()
    {
        if (selectCount == 0)
        {
            stringHold.characterOne = "Dragon";
            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {
            stringHold.characterTwo = "Dragon";
            selectCount++;
        }
    }

    public void AssignAkhlut()
    {
        if (selectCount == 0)
        {
            stringHold.characterOne = "Akhlut";
            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {
            stringHold.characterTwo = "Akhlut";
            selectCount++;
        }
    }

    public void AssignLeshy()
    {
        if (selectCount == 0)
        {
            stringHold.characterOne = "Leshy";
            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {
            stringHold.characterTwo = "Leshy";
            selectCount++;
        }
    }
}
