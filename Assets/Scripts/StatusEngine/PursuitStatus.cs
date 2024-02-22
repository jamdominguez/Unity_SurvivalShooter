using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitStatus : MonoBehaviour {

    private StatusEngine statusEngine;
    private NavMeshController navMeshCtrl;
    private VisionController visionCtrl;
    public float detectionTime = 4f;

    private float currentDetectionTime;

    private void Awake() {
        navMeshCtrl = GetComponent<NavMeshController>();
        visionCtrl = GetComponent<VisionController>();
        statusEngine = GetComponent<StatusEngine>();
    }

    private void OnEnable() {
        currentDetectionTime = 0f;
    }

    private void Update() {

        RaycastHit hit;
        if (!visionCtrl.Detected(out hit, true)) { // no detected
            currentDetectionTime += Time.deltaTime;

            if (currentDetectionTime >= detectionTime) {
                statusEngine.ActivateStatus(statusEngine.alertStatus);
                return;
            }

        } else { // detected
            navMeshCtrl.UpdateTarget();
            currentDetectionTime = 0f;
        }
    }
}
