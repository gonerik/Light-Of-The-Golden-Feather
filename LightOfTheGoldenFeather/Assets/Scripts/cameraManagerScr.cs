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

        float targetaspect = 16.0f / 9.0f;

        float windowaspect = (float)Screen.width / (float)Screen.height;

        float scaleheight = windowaspect / targetaspect;

        Camera camera = Camera.main.GetComponent<Camera>();

        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
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
