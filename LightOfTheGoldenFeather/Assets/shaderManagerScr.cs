using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderManagerScr : MonoBehaviour
{
    [SerializeField] Material mat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetVector("_position", new Vector2(Input.mousePosition.x/Screen.height, Input.mousePosition.y/Screen.height));
        mat.SetFloat("_ringSize", 0.7f);
    }
}
