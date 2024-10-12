using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SMFeather : Feather
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.instance.collectFeather(FeatherManager.instance.smFeatherFuel);
            gameObject.SetActive(false);
        }
    }
}
