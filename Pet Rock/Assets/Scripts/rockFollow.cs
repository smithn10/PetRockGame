using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFollow : MonoBehaviour {
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

    void DisableFollow() { followEnable = false; }
    void EnableFollow() { followEnable = true; }
}
