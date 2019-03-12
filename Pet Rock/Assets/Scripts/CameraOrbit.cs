using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform playerTransform;
    public Transform rockTransform;
    private Vector3 cameraOffset;
    public bool lockOrbit;
    public float hSensitivity = 120;
    public float vSensitivity = 120;
    public float minVAngle = 0;
    public float maxVAngle = 90;
    public Transform smoothingObject;

    void Start()
    {
    }

    void Update()
    {
        if (lockOrbit) { return; } // don't allow camera to orbit if player is in conversation


        // allow camera to orbit horizontally
        float scrollMovement = Input.GetAxis("Mouse X") * Time.deltaTime * hSensitivity;
        transform.RotateAround(smoothingObject.position, Vector3.up, scrollMovement);

        float xrot = transform.rotation.eulerAngles.x;
        float maxRot = maxVAngle - xrot - .1f;
        float minRot = minVAngle - xrot + .1f;
        // allow camera to orbit vertically
        float verticalScroll = Input.GetAxis("Mouse Y") * Time.deltaTime * vSensitivity * -1;
        verticalScroll = Mathf.Min(maxRot, verticalScroll);
        verticalScroll = Mathf.Max(minRot, verticalScroll);
        transform.RotateAround(smoothingObject.position, transform.right, verticalScroll);

        transform.LookAt(smoothingObject);
    }

    // switch camera focus to rock
    public void FocusRock()
    {
        smoothingObject.GetComponent<SmoothFollow>().UpdateTarget(rockTransform.gameObject);
    }

    // switch camera focus to player
    public void FocusPlayer()
    {
        smoothingObject.GetComponent<SmoothFollow>().UpdateTarget(playerTransform.gameObject);
    }
}

