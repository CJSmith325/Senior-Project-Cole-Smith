using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioSource buttonPressing;
    public AudioClip positiveSound;
    public AudioClip negativeSound;

    private void Start()
    {
        buttonPressing = GameObject.FindGameObjectWithTag("Button").GetComponent<AudioSource>();
    }

    IEnumerator waitForButton()
    {
        yield return new WaitForSeconds(2f);
    }

    public void LoadMainMenu()
    {
        buttonPressing.PlayOneShot(negativeSound);
        StartCoroutine(waitForButton());
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadFight()
    {
        buttonPressing.PlayOneShot(positiveSound);
        StartCoroutine(waitForButton());
        SceneManager.LoadScene(FindAnyObjectByType<EnvironmentHolding>().environmentName);
        
    }

    public void RematchFighters()
    {
        buttonPressing.PlayOneShot(positiveSound);
        StartCoroutine(waitForButton());
        SceneManager.LoadScene("Jungle");
    }

    public void LoadCharacterSelect()
    {
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        buttonPressing.PlayOneShot(positiveSound);
        StartCoroutine(waitForButton());
        SceneManager.LoadScene("CharacterSelectScreen");
    }

    public void LoadEnvironmentSelect()
    {
        if (FindAnyObjectByType<EnvironmentHolding>() != null)
        {
            Destroy(FindAnyObjectByType<EnvironmentHolding>().gameObject);
        }
        buttonPressing.PlayOneShot(positiveSound);
        StartCoroutine(waitForButton());
        SceneManager.LoadScene("EnvironmentSelectScreen");
    }

    public void LoadInstructions()
    {
        buttonPressing.PlayOneShot(negativeSound);
        StartCoroutine(waitForButton());
        SceneManager.LoadScene("InstructionsandCredits");
    }

    public void LeaveGame()
    {
        buttonPressing.PlayOneShot(negativeSound);
        StartCoroutine(waitForButton());
        Application.Quit();
    }

}
