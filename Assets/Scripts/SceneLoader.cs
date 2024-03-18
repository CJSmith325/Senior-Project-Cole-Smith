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
        SceneManager.LoadScene(FindAnyObjectByType<EnvironmentHolding>().environmentName);
    }

    public void RematchFighters()
    {
        SceneManager.LoadScene("Jungle");
    }

    public void LoadCharacterSelect()
    {
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        
        SceneManager.LoadScene("CharacterSelectScreen");
    }

    public void LoadEnvironmentSelect()
    {
        if (FindAnyObjectByType<EnvironmentHolding>() != null)
        {
            Destroy(FindAnyObjectByType<EnvironmentHolding>().gameObject);
        }
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
