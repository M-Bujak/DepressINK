using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _nightAmbient;
    [SerializeField]
    private AudioSource _dayAmbient;
    [SerializeField]
    private float _dayAudioTweeningDuration;
    [SerializeField]
    private float _nightAudioTweeningDuration;
    [SerializeField]
    private float _progressThreshold;

    private void Start()
    {
        GameProgressCounter.CounterUpdated += TrySwap;
    }

    private void TrySwap(int coloredObjects)
    {
        if (coloredObjects >= _progressThreshold)
        {
            SwapAmbientSounds();
        }
    }

    private void SwapAmbientSounds()
    {
        GameProgressCounter.CounterUpdated -= TrySwap;

        _dayAmbient.Play();
        TweenSourceVolume(_dayAmbient, 0f,
            1f, _dayAudioTweeningDuration);

        TweenSourceVolume(_nightAmbient,
            1f, 0f, _nightAudioTweeningDuration);
    }

    private void TweenSourceVolume(AudioSource audioSource,
        float startVolume, float targetVolume, float tweeningDuration)
    {
        audioSource.volume = startVolume;

        audioSource.DOFade(targetVolume, tweeningDuration);
    }

    private void OnDestroy()
    {
        GameProgressCounter.CounterUpdated -= TrySwap;
    }
}
