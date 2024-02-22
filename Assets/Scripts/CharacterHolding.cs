using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolding : MonoBehaviour
{
    public string characterOne;
    public string characterTwo;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
