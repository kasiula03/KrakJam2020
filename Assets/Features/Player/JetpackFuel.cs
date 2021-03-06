﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JetpackFuel : MonoBehaviour
{
    [Inject] private PlayerAbilitiesLogic _playerAbilities;

    public float jetpackForce = 15f;

    public float maxFuel = 100;
    public float fuel = 100;
    public float fuelFireSpeed = 50f;
    public float fuelGenerationSpeed = 30f;
    public RectTransform fuelStatusUI;

    private bool _isLaunched = false;

    private void Start()
    {
        _playerAbilities.UnlockedAbilitiesChanged += this.DiplayFuelStatus;
    }
    void FixedUpdate()
    {
        if (!_isLaunched && fuel != maxFuel)
        {
            fuel += Time.deltaTime * fuelGenerationSpeed;
            if (fuel > maxFuel)
                fuel = maxFuel;
            var fuelPercetage = (int)((fuel / maxFuel) * 100);
            UpdateFuelStatus(fuelPercetage);
        }
    }


    private void DiplayFuelStatus()
    {
        foreach (var ability in _playerAbilities.UnlockedAbilities)
        {
            if(ability == Abilities.BindableReaction.Jetpack)
            {
                var canvas = fuelStatusUI.GetComponentInParent<Transform>().GetComponentInParent<Canvas>();
                canvas.enabled = true;
            }
        }
    }
    public void LaunchJetpack()
    {
        _isLaunched = true;
    }
    public void TurnOffJetpack()
    {
        _isLaunched = false;
    }
    public bool IsFuel()
    {
        if (_isLaunched)
        {
            fuel -= Time.deltaTime * fuelFireSpeed;

            if (fuel > 0)
            {
                var fuelPercetage = (int)((fuel / maxFuel) * 100);
                UpdateFuelStatus(fuelPercetage);
                return true;
            }
            else
            {
                fuel = 0;
            }
            UpdateFuelStatus(0);
        }

        return false;
    }
    private void UpdateFuelStatus(int percentage)
    {
        percentage *= 2;
        fuelStatusUI.offsetMax = new Vector2(-(200-percentage), fuelStatusUI.offsetMax.y);
    }

}
