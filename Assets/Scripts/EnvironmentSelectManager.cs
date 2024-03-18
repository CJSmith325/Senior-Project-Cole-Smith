using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentSelectManager : MonoBehaviour
{
    public EnvironmentHolding environmentHolder;

    public void AssignJungle()
    {
        environmentHolder.environmentName = "Jungle";
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }

        SceneManager.LoadScene("CharacterSelectScreen");
    }
    
    public void AssignUrban()
    {
        environmentHolder.environmentName = "Urban";
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }

        SceneManager.LoadScene("CharacterSelectScreen");
    }

    public void AssignDesert()
    {
        environmentHolder.environmentName = "Desert";
        if (FindAnyObjectByType<CharacterHolding>() != null)
        {
            Destroy(FindAnyObjectByType<CharacterHolding>().gameObject);
        }

        SceneManager.LoadScene("CharacterSelectScreen");
    }
}
