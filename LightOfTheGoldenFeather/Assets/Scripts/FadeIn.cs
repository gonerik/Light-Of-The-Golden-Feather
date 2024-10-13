using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DG.Tweening.DOTween;

public class FadeIn : MonoBehaviour
{
    [SerializeField] Image fadeOut;
    [SerializeField] Image fadeIn;

    public void next()
    {
        fadeOut.DOFade(0, 0.3f);
        
        fadeIn.DOFade(1, 0.8f);
        
        
    }
}
