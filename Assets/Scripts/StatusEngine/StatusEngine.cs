using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEngine : MonoBehaviour {
    [Header("Status")]
    public PatrolStatus patrolStatus;
    public AlertStatus alertStatus;
    public PursuitStatus purusitStatus;
    public MonoBehaviour initStatus;
    [Header("Flags")]
    public GameObject alertCube;
    public GameObject pursuitCube;
    public GameObject noDetectionCube;

    MonoBehaviour currentStatus;
    NavMeshController navMeshCtrl;

    private void Awake() {
        navMeshCtrl = GetComponent<NavMeshController>();
    }

    // Start is called before the first frame update
    void Start() {
        ActivateStatus(initStatus);
    }

    public void ActivateStatus(MonoBehaviour newStatus, Transform target) { //For pursuit status it need know the target
        navMeshCtrl.target = target;
        ActivateStatus(newStatus);
    }

    public void ActivateStatus(MonoBehaviour newStatus) {
        if (currentStatus != null) currentStatus.enabled = false;
        currentStatus = newStatus;
        currentStatus.enabled = true;
        UpdateFlag();
    }

    public void UpdateFlag() {
        alertCube.SetActive(currentStatus == alertStatus);
        pursuitCube.SetActive(currentStatus == purusitStatus);
    }

}
