using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManagerScr : MonoBehaviour
{
    [SerializeField] Material mat;

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
    }
}
