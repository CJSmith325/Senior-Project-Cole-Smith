using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyFade : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
