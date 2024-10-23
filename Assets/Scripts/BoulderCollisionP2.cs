using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderCollisionP2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            other.GetComponent<Player1Controller>().player1Health -= 20;
            Destroy(this.gameObject);
        }

        else if (other.tag == "Player2")
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
