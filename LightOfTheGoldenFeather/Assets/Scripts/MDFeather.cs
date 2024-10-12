using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDFeather : Feather
{
    [SerializeField] private AudioSource mdSFX;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.instance.collectFeather(FeatherManager.instance.mdFeatherFuel);
            PlayerMovement.instance.midFeatherTaken = true;
            particle.Play();
            mdSFX.Play();
            stateRenderer(false);
            
        }
    }
}
