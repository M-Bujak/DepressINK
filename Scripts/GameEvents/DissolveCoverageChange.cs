using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveCoverageChange : SubEvent
{
    [SerializeField]
    private float _duration = 1f;
    [SerializeField]
    private Material _material;

    private Material _materialInstance;

    private void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            if (_material.shader == materials[i].shader)
            {
                _materialInstance = materials[i];
            }
        }
    }

    public override void Trigger()
    {
        StartCoverageChange(_duration);
    }

    private void StartCoverageChange(float duration)
    {
        DOTween.To(() => _materialInstance.GetFloat("_Coverage"),
            (x) => _materialInstance.SetFloat("_Coverage", x),
            1f, duration);
    }
}
