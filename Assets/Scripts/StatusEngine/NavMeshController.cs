using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour {

    [HideInInspector]
    public Transform target;

    private NavMeshAgent navMeshAgent;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void UpdateTarget(Vector3 position) { // way points
        navMeshAgent.SetDestination(position);
        //navMeshAgent.Resume(); deprecated
        navMeshAgent.isStopped = false;
    }

    public void UpdateTarget() { // default follow
        navMeshAgent.SetDestination(target.position);
        navMeshAgent.isStopped = false;
    }

    public void Stop() {
        navMeshAgent.isStopped = true;
    }

    public bool HasArrive() {
        return (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) && !navMeshAgent.pathPending;
    }
}
