using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private NewSceneLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();

    }

    private void Awake()
    {
        loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();
    }

    public void StartFight()
    {
        if (loader == null)
        { 
            loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>(); 
        }
        loader.TransitionToScene("EnvironmentSelectScreen");
    }

    public void InstructionsLoad()
    {
        if (loader == null)
        {
            loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();
        }
        loader.TransitionToScene("InstructionsandCredits");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
