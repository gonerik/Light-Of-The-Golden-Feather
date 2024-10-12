using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Feather : MonoBehaviour
{
    protected ParticleSystem particle;
    protected SpriteRenderer renderer;
    protected Collider2D collider;
    private void Start()
    { 
        FeatherManager.feathers.Add(this);
        particle = GetComponentInChildren<ParticleSystem>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    public void stateRenderer(bool state)
    {
        renderer.gameObject.SetActive(state);
        collider.enabled = state;
        
    }
}