using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertStatus : MonoBehaviour {
    public float turnVelocity = 120f;
    public float turnTime = 4f;

    private StatusEngine statusEngine;
    private NavMeshController navMeshCtrl;
    private VisionController visionCtrl;
    private float currentTurnTime;

    private void Awake() {
        statusEngine = GetComponent<StatusEngine>();
        navMeshCtrl = GetComponent<NavMeshController>();
        visionCtrl = GetComponent<VisionController>();
    }

    private void OnEnable() {
        navMeshCtrl.Stop();
        currentTurnTime = 0f;
    }

    private void Update() {
        transform.Rotate(0f, turnVelocity * Time.deltaTime, 0f);
        currentTurnTime += Time.deltaTime;

        if (currentTurnTime >= turnTime) {
            statusEngine.ActivateStatus(statusEngine.patrolStatus);
            return;
        }

        RaycastHit hit;
        if (visionCtrl.Detected(out hit)) {
            statusEngine.ActivateStatus(statusEngine.purusitStatus, hit.transform);
            return;
        }
    }
}
