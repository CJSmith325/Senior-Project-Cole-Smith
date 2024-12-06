using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectBridge : MonoBehaviour
{

    public CharacterSelectManager loader;
    public void endofAnim()
    {
        loader.endofAnim();
    }
}
