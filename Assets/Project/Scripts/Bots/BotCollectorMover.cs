using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PathBuilder), typeof(BotCollector), typeof(ResourceLoader))]
public class BotCollectorMover : MonoBehaviour
{
    private NavMeshAgent _agent;
    private PathBuilder _pathBuilder;
    private float _threshold = 0.001f;

    public event Action Moved;
    public event Action Stayed;

    private void Awake()
    {     
        _pathBuilder = GetComponent<PathBuilder>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {      
        if ((_agent.destination - transform.position).sqrMagnitude < _threshold * _threshold)
        {
            StopMove();
        }
    }

    public void StartMove(Transform target)
    {       
        if (target != null && _pathBuilder.GetFreePath(_agent, target))
        {          
            Moved?.Invoke();
            _agent.SetDestination(target.transform.position);
        }                            
    }

    public void GoHome(Vector3 homePosition)
    {
        Moved?.Invoke();
        _agent.SetDestination(homePosition);      
    }

    private void StopMove()
    {
        Stayed?.Invoke();
        _agent.ResetPath();
    }
}
