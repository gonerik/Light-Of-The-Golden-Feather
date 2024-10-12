using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Feather : MonoBehaviour
{
    protected ParticleSystem particle;
    protected SpriteRenderer renderer;
    protected Collider2D collider;
    protected Light2D light;
    private void Start()
    { 
        FeatherManager.feathers.Add(this);
        particle = GetComponentInChildren<ParticleSystem>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        light = GetComponentInChildren<Light2D>();
    }

    public void stateRenderer(bool state)
    {
        renderer.gameObject.SetActive(state);
        collider.enabled = state;
        light.enabled = state;
        
    }
}