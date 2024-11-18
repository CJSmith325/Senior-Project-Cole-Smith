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
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        buttonSource.PlayOneShot(positiveSound);
        loader.TransitionToScene("CharacterSelectScreen");
    }
    
    public void AssignUrban()
    {
        environmentHolder.environmentName = "Urban";
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        buttonSource.PlayOneShot(positiveSound);
        loader.TransitionToScene("CharacterSelectScreen");
    }

    public void AssignDesert()
    {
        environmentHolder.environmentName = "Desert";
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        buttonSource.PlayOneShot(positiveSound);
        loader.TransitionToScene("CharacterSelectScreen");
    }
}
