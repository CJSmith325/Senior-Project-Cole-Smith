using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLoader : MonoBehaviour
{
    private NewSceneLoader loader;
    private EnvironmentHolding environmentHolder;
    // Start is called before the first frame update
    void Start()
    {
        loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();
        environmentHolder = GameObject.FindAnyObjectByType<EnvironmentHolding>();
    }

    public void StartFight()
    {
        loader.TransitionToScene(environmentHolder.environmentName);
    }

    public void ReselectChars()
    {
        loader.TransitionToScene("CharacterSelectScreen");
    }

    public void mainMenu()
    {
        loader.TransitionToScene("RevampedMain");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
