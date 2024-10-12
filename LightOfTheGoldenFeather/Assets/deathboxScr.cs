using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathboxScr : MonoBehaviour
{
    [SerializeField] private bool instakill;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (instakill)
            {
                other.GetComponent<PlayerMovement>().dieInstantly();
            }
            else
            {
                StartCoroutine(other.GetComponent<PlayerMovement>().GameOver());
            }
        }
    }
}
