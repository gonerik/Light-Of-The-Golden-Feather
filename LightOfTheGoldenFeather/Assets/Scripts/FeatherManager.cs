using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherManager : MonoBehaviour
{
    public static FeatherManager instance;
    public static List<Feather> feathers = new List<Feather>();
    public float smFeatherFuel;
    public float mdFeatherFuel;
    public float bgFeatherFuel;
    public float bigFeatherCost;

    private void Start()
    {
        if(instance == null)
            instance = this;
        else
        {
            Debug.LogError("FeatherManager instance already exists!");
        }
    }

    public static void restart()
    {
        foreach (var i in feathers)
        {
            i.stateRenderer(true);
        }
    }
    
}
