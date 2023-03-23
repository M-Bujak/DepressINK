using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderMaterialSetter : MonoBehaviour
{
    [SerializeField]
    private Material ShaderMaterial;
    [SerializeField]
    private Material BaseMaterialColorForShades;

    void Start()
    {
        SwapMaterialsForAllColorableChildren();
    }

    private void SwapMaterialsForAllColorableChildren()
    {
        Colorable[] childrensColorables = gameObject.GetComponentsInChildren<Colorable>();

        foreach(var colorable in childrensColorables)
        {
            Renderer renderer = colorable.gameObject.GetComponent<Renderer>();
            SetBrightnessModifier(colorable, renderer.material.color);
            SwapMaterial(renderer);
        }
    }

    private void SwapMaterial(Renderer renderer)
    {
        Material tempMaterial = new Material(ShaderMaterial);
        tempMaterial.SetColor("_StartingColor", renderer.material.color);
        renderer.material = tempMaterial;
    }

    private void SetBrightnessModifier(Colorable colorable, Color objectsColor)
    {
        colorable.BrightnessModifier =
            objectsColor.r - BaseMaterialColorForShades.color.r;
    }
}
