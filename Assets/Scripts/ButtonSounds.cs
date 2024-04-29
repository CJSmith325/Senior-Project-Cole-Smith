using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoudns : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] buttonObj = GameObject.FindGameObjectsWithTag("Button");
        if (buttonObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
