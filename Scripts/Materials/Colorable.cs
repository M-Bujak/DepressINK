using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG;
using DG.Tweening;
using System;

public class Colorable : MonoBehaviour
{
    [SerializeField]
    public float BrightnessModifier;
    [SerializeField]
    private float _tweeningDuration = 15f;
    private bool _wasColoredBefore;

    private Renderer _renderer;

    public static Action ChangedColor = delegate { };

    private void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
    }

    public void ChangeColor(Color brushColor, bool isColoredThroughItsOwnCollider)
    {
        if(isColoredThroughItsOwnCollider == true && _wasColoredBefore == false)
        {
            ChangedColor.Invoke();
            _wasColoredBefore = true;
        }

        DOTween.To(() => _renderer.material.GetFloat("_Intensity"),
            (x) => _renderer.material.SetFloat("_Intensity", x),
            1f, _tweeningDuration);

        brushColor.r += BrightnessModifier;
        brushColor.g += BrightnessModifier;
        brushColor.b += BrightnessModifier;

        _renderer.material.SetColor("_BrushColor", brushColor);
    }
}
