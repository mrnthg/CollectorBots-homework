using UnityEngine;
using UnityEngine.AI;

public class PathBuilder : MonoBehaviour
{
    private bool _isPathFree;
    private NavMeshPath _path;

    private void Awake()
    {
        _isPathFree = false;
        _path = new NavMeshPath();
    }

    public bool GetFreePath(NavMeshAgent agent, Transform target)
    {
        NavigatePath(agent, target);
        return _isPathFree;
    }

    private void NavigatePath(NavMeshAgent agent, Transform target)
    {
        if (target)
        {
            agent.SetDestination(target.position);
            agent.CalculatePath(target.position, _path);

            _isPathFree = _path.status == NavMeshPathStatus.PathComplete;          
        }       
    }
}
