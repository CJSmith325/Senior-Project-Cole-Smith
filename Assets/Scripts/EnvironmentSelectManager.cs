using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentSelectManager : MonoBehaviour
{
    public EnvironmentHolding environmentHolder;
    private AudioSource buttonSource;
    public AudioClip positiveSound;
    private NewSceneLoader loader;

    private void Start()
    {
        buttonSource = GameObject.FindGameObjectWithTag("Button").GetComponent<AudioSource>();
        loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();
    }

    public void AssignJungle()
    {
        environmentHolder.environmentName = "Jungle";
        loader.LoadCharacterSelect();
    }
    
    public void AssignUrban()
    {
        environmentHolder.environmentName = "Urban";
        loader.LoadCharacterSelect();
    }

    public void AssignDesert()
    {
        environmentHolder.environmentName = "Desert";
        loader.LoadCharacterSelect();
    }
}
