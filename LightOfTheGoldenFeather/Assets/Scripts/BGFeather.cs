using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BGFeather : Feather
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.instance.collectFeather(FeatherManager.instance.bgFeatherFuel);
            PlayerMovement.instance.bigFeatherTaken = true;
            gameObject.SetActive(false);
        }
    }
}
