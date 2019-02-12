using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {
    public GameObject player;
    public GameObject cam;
    public GameObject[] children;
    public GameObject activeState;
    private bool inRange = false;
    private bool leverActive = false;

    void Update() {
        if (inRange) {
            if ((Input.GetKeyDown(KeyCode.E)) && (!leverActive)) { // interact button was pressed when lever was not active
                for(int i=0; i<children.Length; i++) {
                    children[i].GetComponent<Collider>().enabled = true;
                    children[i].GetComponent<MeshRenderer>().enabled = true;
                }
                activeState.GetComponent<Collider>().enabled = true;
                activeState.GetComponent<MeshRenderer>().enabled = true;
                GetComponent<Collider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
                leverActive = true;
            } else if ((Input.GetKeyDown(KeyCode.E)) && (leverActive)) { // interact button was pressed when lever was active
                for(int i=0; i<children.Length; i++) {
                    children[i].GetComponent<Collider>().enabled = false;
                    children[i].GetComponent<MeshRenderer>().enabled = false;
                }
                activeState.GetComponent<Collider>().enabled = false;
                activeState.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = true;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                leverActive = false;
            }
        }
    }


    void OnTriggerEnter(Collider col) {
        if (col.name == "Character") { // player collided with object
            inRange = true; // update in range when entering lever trigger range
        }
    }

    void OnTriggerExit(Collider col) {
        inRange = false; // update in range when leaving lever trigger range
    }
}
