using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField]
    private Material _daySkyboxTemplate;
    [SerializeField]
    private Material _nightSkyboxTemplate;
    [SerializeField]
    private int _progressTresholdForSkyboxChange = 50;
    [SerializeField]
    private Transform _directionalLight;
    [SerializeField]
    private float _lightXRotationWhenSunUp = 15f;
    [SerializeField]
    private float _sunRisingTime = 5f;
    [SerializeField]
    private CityLightsController _cityLightsController;

    private Material _currentSkybox;

    private void Awake()
    {
        GameProgressCounter.CounterUpdated += HandleCounterUpdate;
        _currentSkybox = RenderSettings.skybox;

        ResetWeather();
    }

    private void HandleCounterUpdate(int progress)
    {
        if (progress >= _progressTresholdForSkyboxChange)
        {
            GameProgressCounter.CounterUpdated -= HandleCounterUpdate;

            ChangeWeatherToDay();
            ChangeSunPosition();
        }
    }

    private void ChangeSunPosition()
    {
        float y = _directionalLight.transform.rotation.eulerAngles.y;

        _directionalLight.DORotate(
            new Vector3(_lightXRotationWhenSunUp,
            y, 0f), _sunRisingTime);
    }

    private void ChangeWeatherToDay()
    {
        CopyShaderColorParameter(_daySkyboxTemplate, _currentSkybox, "_SkyGradientTop");
        _cityLightsController.BeginDisablingAllLights();
        TurnOffFog();
    }

    private void CopyShaderColorParameter(Material from, Material to, string name)
    {
        to.DOColor(from.GetColor(name), name, 5f).From(to.GetColor(name));
    }

    private void TurnOffFog()
    {
        DOTween.To(() => RenderSettings.fogDensity,
            (x) => RenderSettings.fogDensity = x,
            0f, _sunRisingTime);
    }

    private void ResetWeather()
    {
        string changedParameterName = "_SkyGradientTop";

        Color colorToCopy = _nightSkyboxTemplate.GetColor(changedParameterName);
        _currentSkybox.SetColor(changedParameterName, colorToCopy);
    }

    private void OnApplicationQuit()
    {
        ResetWeather();
    }

    private void OnDestroy()
    {
        GameProgressCounter.CounterUpdated -= HandleCounterUpdate;
    }
}
