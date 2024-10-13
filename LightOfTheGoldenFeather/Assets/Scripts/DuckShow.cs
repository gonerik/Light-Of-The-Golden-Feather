using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DuckShow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private void OnTriggerEnter2D(Collider2D other)
    {
        target.DOMoveY(-86f, 2f).SetEase(Ease.OutBounce);
        StartCoroutine(anim());

    }

    private IEnumerator anim()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
