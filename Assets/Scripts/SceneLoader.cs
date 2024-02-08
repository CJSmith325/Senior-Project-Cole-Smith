using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadFight()
    {
        SceneManager.LoadScene("FightScene");
    }

    public void RematchFighters()
    {
        SceneManager.LoadScene("FightScene");
    }

    public void LoadCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelectScreen");
    }

    public void LoadEnvironmentSelect()
    {
        SceneManager.LoadScene("EnvironmentSelectScreen");
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("InstructionsandCredits");
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

}
