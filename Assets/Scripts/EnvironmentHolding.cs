using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHolding : MonoBehaviour
{
    public string environmentName;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
