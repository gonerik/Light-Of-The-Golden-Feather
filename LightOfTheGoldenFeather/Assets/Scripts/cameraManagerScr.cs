using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManagerScr : MonoBehaviour
{
    [SerializeField] GameObject[] vCameras;
    int index = 0;
    private void Start()
    {
        UpdateCams();
    }

    public void ResetToFirst()
    {
        index = 0;
        UpdateCams();
    }
    void UpdateCams()
    {
        for (int i = 0; i < vCameras.Length; i++)
        {
            if (i == index)
            {
                vCameras[i].SetActive(true);
            }
            else
            {
                vCameras[i].SetActive(false);
            }
        }
    }
    public void nextCam()
    {
        index++;

        if (index == vCameras.Length)
        {
            index = 0;
        }
        UpdateCams();
    }
}
