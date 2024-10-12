using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Feather : MonoBehaviour
    {
        private void Start()
        {
            FeatherManager.feathers.Add(this);
        }
    }
}