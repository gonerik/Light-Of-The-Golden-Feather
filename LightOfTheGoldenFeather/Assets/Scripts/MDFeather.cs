using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDFeather : MonoBehaviour
{
    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            PlayerMovement.instance.collectFeather(2f);
            PlayerMovement.instance.midFeatherTaken = true;
            yield return new WaitForSeconds(0.2f);
            PlayerMovement.instance.midFeatherTaken = false;
            Destroy(gameObject);
            
        }
    }
}
