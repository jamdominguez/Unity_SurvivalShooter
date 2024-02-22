using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStatus : MonoBehaviour {

    public Transform[] wayPoints;

    private StatusEngine statusEngine;
    private NavMeshController navMeshCtrl;
    private VisionController visionCtrl;
    private int nextWayPoint;


    private void Awake() {
        navMeshCtrl = GetComponent<NavMeshController>();
        visionCtrl = GetComponent<VisionController>();
        statusEngine = GetComponent<StatusEngine>();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        if(visionCtrl.Detected(out hit)) {
            statusEngine.ActivateStatus(statusEngine.purusitStatus, hit.transform);
            return;
        }

        if (navMeshCtrl.HasArrive()) {
            nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
            UpdateWayPoint();
        }
    }

    private void OnEnable() {
        UpdateWayPoint();
    }

    private void UpdateWayPoint() {
        navMeshCtrl.UpdateTarget(wayPoints[nextWayPoint].position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && enabled) {
            statusEngine.ActivateStatus(statusEngine.alertStatus);
        }
    }
}
