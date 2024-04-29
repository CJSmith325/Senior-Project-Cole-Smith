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

    private void Start()
    {
        buttonSource = GameObject.FindGameObjectWithTag("Button").GetComponent<AudioSource>();

    }

    public void AssignJungle()
    {
        environmentHolder.environmentName = "Jungle";
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        buttonSource.PlayOneShot(positiveSound);
        SceneManager.LoadScene("CharacterSelectScreen");
    }
    
    public void AssignUrban()
    {
        environmentHolder.environmentName = "Urban";
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        buttonSource.PlayOneShot(positiveSound);
        SceneManager.LoadScene("CharacterSelectScreen");
    }

    public void AssignDesert()
    {
        environmentHolder.environmentName = "Desert";
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        buttonSource.PlayOneShot(positiveSound);
        SceneManager.LoadScene("CharacterSelectScreen");
    }
}
