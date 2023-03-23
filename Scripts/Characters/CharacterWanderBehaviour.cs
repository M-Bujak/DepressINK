using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterWanderBehaviour : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _maxWalkDistance = 10f;
    [SerializeField]
    private float _minWaitTime = 5f;
    [SerializeField]
    private float _maxWaitTime = 20f;

    private bool _isWaiting;
    private bool _isWalking;
    private bool _isHappy;

    void Start()
    {
        GoToARandomPosition();
        _navMeshAgent.isStopped = false;
    }

    private void Update()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _isWalking = false;
        }

        if (_isWaiting == false && _isWalking == false)
        {
            StartCoroutine(WaitBeforeWandering());
        }
    }

    private Vector3 GetRandomPositionOnNavmesh()
    {
        Vector3 direction = Random.insideUnitSphere * _maxWalkDistance;
        direction += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(direction, out hit, Random.Range(1f, _maxWalkDistance), 1);

        return hit.position;
    }

    private IEnumerator WaitBeforeWandering()
    {
        if (_isWalking == true)
        {
            yield break;
        }

        _isWalking = false;
        _isWaiting = true;
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsWaiting", true);

        float passedTime = 0;
        float timeToWait = Random.Range(_minWaitTime, _maxWaitTime);

        while (passedTime < timeToWait)
        {
            yield return null;
            passedTime += Time.deltaTime;
        }

        GoToARandomPosition();
    }

    private void GoToARandomPosition()
    {
        _navMeshAgent.destination = GetRandomPositionOnNavmesh();

        _isWalking = true;
        _isWaiting = false;
        _animator.SetBool("IsWalking", true);
        _animator.SetBool("IsWaiting", false);
    }

    public void ChangeMoodToHappy()
    {
        _isHappy = true;
        _animator.SetBool("IsHappy", true);
    }
}
