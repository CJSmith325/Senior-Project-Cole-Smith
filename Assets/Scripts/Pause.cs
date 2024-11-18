using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool paused = false;
    public Canvas PauseCanvas;
    private AudioSource buttonPressing;
    public AudioClip posSound;
    public AudioClip negSound;
    private NewSceneLoader loader;
    private void Start()
    {
        buttonPressing = GameObject.FindGameObjectWithTag("Button").GetComponent<AudioSource>();
        loader = GameObject.FindAnyObjectByType<NewSceneLoader>().GetComponent<NewSceneLoader>();
    }
    //public Canvas mainCanv;
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu();
        }
    }



    public void pauseMenu()
    {
        if (!paused)
        {

            PauseCanvas.gameObject.SetActive(true);
            //mainCanv.gameObject.SetActive(false);
            buttonPressing.PlayOneShot(negSound);
            Time.timeScale = 0;

        }
        else
        {
            buttonPressing.PlayOneShot(posSound);
            Time.timeScale = 1;
            PauseCanvas.gameObject.SetActive(false);
            //mainCanv.gameObject.SetActive(true);
        }

        paused = !paused;
    }

    public void RestartFight()
    {
        buttonPressing.PlayOneShot(posSound);
        Time.timeScale = 1;
        loader.TransitionToScene(FindAnyObjectByType<EnvironmentHolding>().environmentName);
    }

    public void ReturnCharacterSelect()
    {
        buttonPressing.PlayOneShot(posSound);
        
        Destroy(GameObject.Find("CharacterHolder"));
        loader.TransitionToScene("CharacterSelectScreen");
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        buttonPressing.PlayOneShot(negSound);
        
        loader.TransitionToScene("RevampedMain");
        Time.timeScale = 1;
    }


    public void exit()
    {
        buttonPressing.PlayOneShot(negSound);
        Application.Quit();
    }
}
