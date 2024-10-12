using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRespawn : MonoBehaviour
{
    public float startAura = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.instance.setRespawn(this);
            PlayerMovement.instance.setLockPlayer(false);
            SettingsMenu.setSlider(SettingsMenu.difficulty);
        }
    }
}
