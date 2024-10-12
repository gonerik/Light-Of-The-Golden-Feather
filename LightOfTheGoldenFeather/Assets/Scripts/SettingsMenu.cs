using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public static Difficulty difficulty;
    private static Slider slider;
    
    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        gameObject.SetActive(false);
        setSlider(difficulty);
        
    }
    

    public static void setSlider(Difficulty value)
    {
        switch (value)
        {
            case Difficulty.Easy:
                slider.value = 0;
                break;
            case Difficulty.Mid:
                slider.value = 1;
                break;
            case Difficulty.Hard:
                slider.value = 2;
                break;
        }
    }

    [SerializeField] private AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void difficultySelection(float difficulty)
    {
        switch (difficulty)
        {
            case 0: 
                text.text = "Difficulty: Easy";
                Debug.Log("Difficulty 0");
                FeatherManager.instance.setDifficulty(Difficulty.Easy);
                break;
            case 1: 
                text.text = "Difficulty: Medium";
                Debug.Log("Difficulty 1");
                FeatherManager.instance.setDifficulty(Difficulty.Mid);
                break;
            case 2: 
                text.text = "Difficulty: Hard";
                Debug.Log("Difficulty 2");
                FeatherManager.instance.setDifficulty(Difficulty.Hard);
                break;
        }
    }
}

