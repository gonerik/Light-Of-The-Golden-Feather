using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightManagerScr : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Light2D light;
    [SerializeField] PlayerMovement PM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Camera.main.WorldToScreenPoint(transform.position).x / Screen.height;
        float y = Camera.main.WorldToScreenPoint(transform.position).y / Screen.height;
        mat.SetVector("_position", new Vector2(x, y));
        mat.SetFloat("_ringSize", PM.auraScale*0.2f+0.5f);
        light.pointLightInnerRadius = PM.auraScale*15f;
        light.pointLightOuterRadius = PM.auraScale* 20f;
    }
}
