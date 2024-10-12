using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MDFeather : Feather
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);
            PlayerMovement.instance.collectFeather(FeatherManager.instance.mdFeatherFuel);
            PlayerMovement.instance.midFeatherTaken = true;
            
        }
    }
}
