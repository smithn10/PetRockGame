using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform playerTransform;
    public Transform rockTransform;
    private Vector3 cameraOffset;
    public float orbitSpeed;
    [Range(0.01f, 1.00f)]
    public float smoothness;
    private Transform currentTransform;

    void Start() {
        currentTransform = playerTransform; // initialize to player at start
        cameraOffset = transform.position - currentTransform.position;
    }

    void Update() {
        // follow player by camera offset
        Vector3 positionUpdate = currentTransform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, positionUpdate, smoothness);

        // allow camera to orbit horizontally
        float scrollMovement = Input.GetAxis("Mouse X") * Time.deltaTime * orbitSpeed;
        Quaternion turnCameraAngle = Quaternion.AngleAxis(scrollMovement, Vector3.up);
        cameraOffset = turnCameraAngle * cameraOffset;

        // set player's Y rotation to the camera's Y rotation
        float cameraRotationY = transform.eulerAngles.y;
        currentTransform.eulerAngles = new Vector3(0f, cameraRotationY, 0f);
        transform.LookAt(currentTransform);
    }

    // switch camera focus to rock
    void FocusRock() { currentTransform = rockTransform; }

    // switch camera focus to player
    void FocusPlayer() { currentTransform = playerTransform; }
}

