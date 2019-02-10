using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFollow : MonoBehaviour {
    public Transform player;
    public GameObject cam;
    public float speed = 4f;
    public float followDistance = 3f;
    public bool followEnable = true;
    private float currentDistanceAway;
    private bool isPlayer = true;

    void Update() {
        currentDistanceAway = Vector3.Distance(player.transform.position, transform.position);
        isPlayer = (cam.GetComponent<CameraFollow>().currentTransform == player);

        if ((currentDistanceAway > followDistance) && (followEnable)) {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        } else if ((currentDistanceAway <= followDistance) && (!followEnable) && (isPlayer))  {
            EnableFollow();
        }

        if((followEnable) && (!isPlayer)) {
            DisableFollow();
        }
    }

    // disable the rock's ai follow
    void DisableFollow() { followEnable = false; }

    // enable the rock's ai follow
    void EnableFollow() { followEnable = true; }
}
