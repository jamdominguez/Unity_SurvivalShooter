using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour {

    public Transform eyes;
    public float range = 20f;
    public Vector3 offset = new Vector3(0, 0.75f, 0);

    private NavMeshController navMeshCtrl;

    private void Awake() {
        navMeshCtrl = GetComponent<NavMeshController>();
    }

    public bool Detected(out RaycastHit hit, bool lookAtPlayer = false) {
        Vector3 fwd = lookAtPlayer ? ((navMeshCtrl.target.position + offset) - eyes.position) : eyes.forward;
        return Physics.Raycast(eyes.position, fwd, out hit, range) && hit.collider.CompareTag("Player");
    }
}
