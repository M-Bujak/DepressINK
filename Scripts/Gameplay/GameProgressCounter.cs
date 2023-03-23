using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressCounter : MonoBehaviour
{
    public int ColoredObjectsCounter { get; private set; }

    public static Action<int> CounterUpdated = delegate { };

    private void Start()
    {
        Colorable.ChangedColor += UpdateCounter;
        MultipleObjectsToColor.ChangedColor += UpdateCounter;
    }

    public void UpdateCounter()
    {
        ColoredObjectsCounter++;
        Debug.Log(ColoredObjectsCounter);
        CounterUpdated.Invoke(ColoredObjectsCounter);
    }

    private void OnDestroy()
    {
        Colorable.ChangedColor -= UpdateCounter;
        MultipleObjectsToColor.ChangedColor -= UpdateCounter;
    }
}
