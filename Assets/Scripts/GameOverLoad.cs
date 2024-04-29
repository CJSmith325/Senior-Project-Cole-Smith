using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverLoad : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    
    // Start is called before the first frame update
    void Start()
    {
        victoryText.text = GameControl.victoryText.ToString();
        
    }

}    
