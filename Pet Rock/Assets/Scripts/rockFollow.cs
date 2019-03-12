using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFollow : MonoBehaviour {
    public Transform player;
    public GameObject cam;
    public float speed = 4f;
    public float followDistance = 3f;
    public bool followEnable = true;
    public bool canMove = false;
    private float currentDistanceAway;
    private bool isPlayer = true;

    void Update() {
        if (canMove) {
            currentDistanceAway = Vector3.Distance(player.transform.position, transform.position);

            if ((currentDistanceAway > followDistance) && (followEnable)) {
                transform.LookAt(player);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }/* else if ((currentDistanceAway <= followDistance) && (!followEnable) && (isPlayer)) {
                EnableFollow();
            }*/
            
        }
    }

    // disable the rock's ai follow
    void DisableFollow() { followEnable = false; }

    // enable the rock's ai follow
    void EnableFollow() { followEnable = true; }

    // give rock the ability to move
    void AddMovement() { canMove = true; }
}
