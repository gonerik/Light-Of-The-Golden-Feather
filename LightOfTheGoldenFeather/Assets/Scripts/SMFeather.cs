using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMFeather : Feather
{
    [SerializeField] private AudioSource smSFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.instance.collectFeather(FeatherManager.instance.smFeatherFuel);
            particle.Play();
            smSFX.Play();
            stateRenderer(false);
        }
    }
}
