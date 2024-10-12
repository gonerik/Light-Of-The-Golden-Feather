using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScr : MonoBehaviour
{
    [SerializeField] private Transform endPt;
    [SerializeField] private cameraManagerScr cMS;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = endPt.position;

            cMS.nextCam();            
        }
    }
}
