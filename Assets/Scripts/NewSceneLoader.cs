using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class NewSceneLoader : MonoBehaviour
{
    public static NewSceneLoader Instance; // Singleton instance
    public CanvasGroup fadeCanvasGroup;            // Canvas group for fade effect
    public float fadeDuration = 1.0f;
    private AudioSource buttonPressing;
    public AudioClip positiveSound;
    public AudioClip negativeSound;

    private void Awake()
    {
        
        fadeCanvasGroup = GameObject.FindAnyObjectByType<CanvasGroup>().GetComponent<CanvasGroup>();
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        fadeCanvasGroup = GameObject.FindAnyObjectByType<CanvasGroup>().GetComponent<CanvasGroup>();
        buttonPressing = GameObject.FindGameObjectWithTag("Button").GetComponent<AudioSource>();
        // Start fully transparent
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0;
        }
    }

    IEnumerator waitForButton()
    {
        yield return new WaitForSeconds(2f);
    }

    public void LoadMainMenu()
    {
        fadeCanvasGroup.GetComponent<Image>().raycastTarget = true;
        buttonPressing.PlayOneShot(negativeSound);
        StartCoroutine(waitForButton());
        TransitionToScene("MainMenu");
    }

    public void LoadFight()
    {
        fadeCanvasGroup.GetComponent<Image>().raycastTarget = true;
        buttonPressing.PlayOneShot(positiveSound);
        StartCoroutine(waitForButton());
        TransitionToScene(FindAnyObjectByType<EnvironmentHolding>().environmentName);

    }

    public void RematchFighters()
    {
        fadeCanvasGroup.GetComponent<Image>().raycastTarget = true;
        buttonPressing.PlayOneShot(positiveSound);
        StartCoroutine(waitForButton());
        TransitionToScene("Jungle");
    }

    public void LoadCharacterSelect()
    {
        fadeCanvasGroup.GetComponent<Image>().raycastTarget = true;
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }
        buttonPressing.PlayOneShot(positiveSound);
        StartCoroutine(waitForButton());
        TransitionToScene("CharacterSelectScreen");
    }

    public void LoadEnvironmentSelect()
    {
        fadeCanvasGroup.GetComponent<Image>().raycastTarget = true;
        if (FindAnyObjectByType<EnvironmentHolding>() != null)
        {
            Destroy(FindAnyObjectByType<EnvironmentHolding>().gameObject);
        }
        buttonPressing.PlayOneShot(positiveSound);
        StartCoroutine(waitForButton());
        TransitionToScene("EnvironmentSelectScreen");
        
    }

    public void LoadInstructions()
    {
        fadeCanvasGroup.GetComponent<Image>().raycastTarget = true;
        buttonPressing.PlayOneShot(negativeSound);
        StartCoroutine(waitForButton());
        TransitionToScene("InstructionsandCredits");
    }

    public void LeaveGame()
    {
        fadeCanvasGroup.GetComponent<Image>().raycastTarget = true;
        buttonPressing.PlayOneShot(negativeSound);
        StartCoroutine(waitForButton());
        Application.Quit();
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(PerformSceneTransition(sceneName));
    }

    private IEnumerator PerformSceneTransition(string sceneName)
    {
        // Ensure the canvas starts visible
        fadeCanvasGroup.alpha = 0;
        fadeCanvasGroup.gameObject.SetActive(true); // Activate the fade canvas
        
        // Fade out (to black)
        yield return StartCoroutine(Fade(1));

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        

        // Wait for one frame to ensure the scene is fully loaded
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        fadeCanvasGroup = GameObject.FindFirstObjectByType<CanvasGroup>().GetComponent<CanvasGroup>();
        
        fadeCanvasGroup.gameObject.SetActive(true);

        if (sceneName == "GameOverScreen")
        {
            Time.timeScale = 1;
        }
        // Fade in (from black)
        yield return StartCoroutine(Fade(0));

        Debug.Log(fadeCanvasGroup.gameObject.name);

        // Deactivate the fade canvas after fading in
        fadeCanvasGroup.gameObject.SetActive(false);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvasGroup == null) yield break;

        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}

