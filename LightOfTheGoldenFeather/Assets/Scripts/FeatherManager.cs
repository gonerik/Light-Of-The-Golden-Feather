using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class FeatherManager : MonoBehaviour
{
    public static FeatherManager instance;
    public static List<Feather> feathers = new List<Feather>();
    public float smFeatherFuel;
    public float mdFeatherFuel;
    public float bgFeatherFuel;
    public float bigFeatherCost;
    private Difficulty difficulty;
    [SerializeField] private DifficultyS difficultyS;
    

    private void Start()
    {
        difficulty = Difficulty.Mid;
        if(instance == null)
            instance = this;
        else
        {
            Debug.LogError("FeatherManager instance already exists!");
        }
        smFeatherFuel = difficultyS.SMLenghtMid;
        mdFeatherFuel = difficultyS.MDLenghtMid;
        bgFeatherFuel = difficultyS.BGLenghtMid;
    }

    public void setDifficulty(Difficulty difficulty)
    {
        this.difficulty = difficulty;
        switch (this.difficulty)
        {
            case Difficulty.Easy:
                smFeatherFuel = difficultyS.SMLenghtEasy;
                mdFeatherFuel = difficultyS.MDLenghtEasy;
                bgFeatherFuel = difficultyS.BGLenghtEasy;
                break;
            case Difficulty.Mid:
                smFeatherFuel = difficultyS.SMLenghtMid;
                mdFeatherFuel = difficultyS.MDLenghtMid;
                bgFeatherFuel = difficultyS.BGLenghtMid;
                break;
            case Difficulty.Hard:
                smFeatherFuel = difficultyS.SMLenghtHard;
                mdFeatherFuel = difficultyS.MDLenghtHard;
                bgFeatherFuel = difficultyS.BGLenghtHard;
                break;
        }
    }

    public static void restart()
    {
        foreach (var i in feathers)
        {
            i.stateRenderer(true);
        }
    }

    public void reset()
    {
        feathers.Clear();
    }
}
