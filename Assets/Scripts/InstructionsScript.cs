using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstructionsScript : MonoBehaviour
{
    private NewSceneLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();
        
    }

    public void StartFight()
    {
        loader.TransitionToScene("EnvironmentSelectScreen");
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
