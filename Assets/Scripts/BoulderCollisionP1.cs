using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderCollisionP1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2")
        {
            other.GetComponent<Player2Controller>().player2Health -= 20;
            
            Destroy(this.gameObject);
        }

        else if (other.tag == "Player1")
        {

        }
        
        else if (other.tag == "Ground")
        {
            Destroy(this.gameObject);
        }

        else
        {
            
        }
    }
}
