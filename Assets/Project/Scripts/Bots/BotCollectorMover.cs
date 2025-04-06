using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PathBuilder), typeof(BotCollector), typeof(ResourceLoader))]
public class BotCollectorMover : MonoBehaviour
{
    private BotCollector _botCollector;
    private NavMeshAgent _agent;
    private PathBuilder _pathBuilder;
    private ResourceLoader _resourceLoader;

    public event Action Moved;
    public event Action Stayed;

    public NavMeshAgent Agent => _agent;

    private void Awake()
    {
        _botCollector = GetComponent<BotCollector>();
        _pathBuilder = GetComponent<PathBuilder>();
        _resourceLoader = GetComponent<ResourceLoader>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _resourceLoader.Loaded += GoHome;       
    }

    private void OnDisable()
    {
        _resourceLoader.Loaded -= GoHome;
    }

    private void Update()
    {
        if (_agent.destination == transform.position)
        {
            StopMove();
        }
    }

    public void StartMove(Transform target)
    {       
        if (target != null && _pathBuilder.CheckPath(_agent, target))
        {          
            Moved?.Invoke();
            _agent.SetDestination(target.transform.position);
        }                            
    }

    public void GoHome()
    {
        Moved?.Invoke();
        _agent.SetDestination(_botCollector.HomeTransform);      
    }

    private void StopMove()
    {
        Stayed?.Invoke();
        _agent.ResetPath();
    }
}
