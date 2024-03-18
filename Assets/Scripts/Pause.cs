using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool paused = false;
    public Canvas PauseCanvas;
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
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
            PauseCanvas.gameObject.SetActive(false);
            //mainCanv.gameObject.SetActive(true);
        }

        paused = !paused;
    }

    public void RestartFight()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(FindAnyObjectByType<EnvironmentHolding>().environmentName);
    }

    public void ReturnCharacterSelect()
    {
        Time.timeScale = 1;
        Destroy(GameObject.Find("CharacterHolder"));
        SceneManager.LoadScene("CharacterSelectScreen");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


    public void exit()
    {
        Application.Quit();
    }
}
