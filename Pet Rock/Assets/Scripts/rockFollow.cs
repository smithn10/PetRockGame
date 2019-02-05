using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFollow : MonoBehaviour {
    public Transform player;
    public float speed = 4f;
    public float followDistance = 3f;
    public bool followEnable = true;
    private float currentDistanceAway;

    void Update() {
        transform.LookAt(player);
        currentDistanceAway = Vector3.Distance(player.transform.position, transform.position);

        if ((currentDistanceAway > followDistance) && (followEnable)) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    // disable the rock's ai follow
    void DisableFollow() { followEnable = false; }

    // enable the rock's ai follow
    void EnableFollow() { followEnable = true; }
}
