using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DieAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    private float _animationDuration;

    private void Start()
    {
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            _animationDuration = Mathf.Max(_animationDuration, particle.duration);
        }
    }

    public IEnumerator Execute(Action afterAnimation)
    {
        _explosion.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        afterAnimation.Invoke();
    }
}
