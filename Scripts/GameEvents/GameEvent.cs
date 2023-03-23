using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    [SerializeField]
    private int _gameProgressThreshold;
    [SerializeField]
    private SubEvent[] _gameSubEvents;

    private void Start()
    {
        GameProgressCounter.CounterUpdated += TryTrigger; 
    }

    protected virtual void TryTrigger(int gameProgress)
    {
        if(gameProgress > _gameProgressThreshold)
        {
            for(int i = 0; i < _gameSubEvents.Length; i++)
            {
                _gameSubEvents[i].Trigger();
            }
        }
    }

    private void OnDestroy()
    {
        GameProgressCounter.CounterUpdated -= TryTrigger;
    }
}
