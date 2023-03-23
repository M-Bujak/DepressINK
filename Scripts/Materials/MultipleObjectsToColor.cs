using System;
using UnityEngine;

public class MultipleObjectsToColor : MonoBehaviour
{
    [SerializeField]
    public Colorable[] Colorables;

    private bool _hasBeenColoredBefore;

    public static Action ChangedColor = delegate { };

    public void ChangeColors(Color brushColor)
    {
        if(_hasBeenColoredBefore == false)
        {
            ChangedColor.Invoke();
        }

        foreach(var colorable in Colorables)
        {
            colorable.ChangeColor(brushColor, false);
        }

        _hasBeenColoredBefore = true;
    }
}
