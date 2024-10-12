using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    [SerializeField] private int sceneID;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FeatherManager.instance.reset();
            SceneManager.LoadSceneAsync(sceneID);
        }
    }
}
