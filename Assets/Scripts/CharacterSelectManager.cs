using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelectManager : MonoBehaviour
{
    private int selectCount;
    
    public TextMeshProUGUI playerText; 
    // Start is called before the first frame update
    void Start()
    {
        selectCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectCount == 2)
        {
            SceneManager.LoadScene("FightScene");
        }
    }

    public void AssignSasquatch()
    {
        if (selectCount == 0)
        {

            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {

            selectCount++;
        }
    }

    public void AssignMothman()
    {
        if (selectCount == 0)
        {

            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {

            selectCount++;
        }
    }

    public void AssignDragon()
    {
        if (selectCount == 0)
        {

            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {

            selectCount++;
        }
    }

    public void AssignAkhlut()
    {
        if (selectCount == 0)
        {

            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {

            selectCount++;
        }
    }

    public void AssignLeshy()
    {
        if (selectCount == 0)
        {

            selectCount++;
            playerText.text = "Player 2";
            return;
        }

        if (selectCount == 1)
        {

            selectCount++;
        }
    }
}
