using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private float _tweeningDuration = 5f;
    [SerializeField]
    private int _progressChangeTreshold = 50;

    private CharacterColorRestoreBehaviour[] _restoreBehaviours;

    private void Start()
    {
        _restoreBehaviours = GetComponentsInChildren<CharacterColorRestoreBehaviour>();
        GameProgressCounter.CounterUpdated += OnCounterUpdate;
    }

    private void OnCounterUpdate(int numberOfColoredObjects)
    {
        if (numberOfColoredObjects > _progressChangeTreshold)
        {
            RestoreColorToCharacters(0.35f * numberOfColoredObjects / _progressChangeTreshold);
        }
    }

    private void RestoreColorToCharacters(float intensity)
    {
        for (int i = 0; i < _restoreBehaviours.Length; i++)
        {
            _restoreBehaviours[i].RestoreColor(intensity, _tweeningDuration);
        }
    }
}
