using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to enable colliders of object on trigger enter
public class ColliderTrigger : MonoBehaviour {
    public GameObject[] objects;
    public GameObject player;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            for (int i = 0; i < objects.Length; i++) {
                objects[i].GetComponent<ActivateConversation>().enabled = true;
            }
        }
    }
}
