using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnableDecalOnProgress : MonoBehaviour
{
    [SerializeField]
    private int _progressTreshold;
    [SerializeField]
    private DecalProjector _decalProjector;

    private void Start()
    {
        GameProgressCounter.CounterUpdated += TryEnable;
    }

    private void TryEnable(int objectsPainted)
    {
        if(objectsPainted >= _progressTreshold)
        {
            _decalProjector.enabled = true;
        }
    }

    private void OnDestroy()
    {
        GameProgressCounter.CounterUpdated -= TryEnable;
    }
}
