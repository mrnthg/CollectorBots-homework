using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PathBuilder : MonoBehaviour
{
    private bool _isPathFree;
    private NavMeshPath _path;
    private Coroutine _coroutine;

    private void Awake()
    {
        _isPathFree = false;
        _path = new NavMeshPath();
    }

    public bool GetFreePath(NavMeshAgent agent, Transform target)
    {
        _coroutine = StartCoroutine(NavigatePath(agent, target));
        return _isPathFree;
    }

    private IEnumerator NavigatePath(NavMeshAgent agent, Transform target)
    {
        if (target)
        {
            agent.SetDestination(target.position);
            agent.CalculatePath(target.position, _path);

            if (_path.status == NavMeshPathStatus.PathComplete)
            {
                _isPathFree = true;
            }
            else
            {
                _isPathFree = false;
            }
        }

        yield return null;
    }
}
