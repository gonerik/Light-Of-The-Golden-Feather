using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFeather : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.instance.collectFeather(2f);
            PlayerMovement.instance.bigFeatherTaken = true;
            Destroy(gameObject);
        }
    }
}
