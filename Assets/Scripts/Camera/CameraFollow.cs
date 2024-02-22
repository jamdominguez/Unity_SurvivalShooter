using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    private void Start() {
        Debug.Log("target.transform.position: " + target.transform.position);
        Debug.Log("target.position: " + target.position);
        offset = transform.position - target.transform.position;
    }

    private void FixedUpdate() {
        // the player is moved in the FixedUpdate method
        Vector3 targetCameraPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
    }
}
