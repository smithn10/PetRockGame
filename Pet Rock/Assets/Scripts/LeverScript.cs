using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {
    public GameObject cam;
    public GameObject target;
    public GameObject activeState;
    public Wiring lightingPath = null;
    private bool inRange = false;
    private bool leverActive = false;

    void Update() {
        //toggle active state of stairs, lever, and other lever
        if (inRange && Input.GetKeyDown(KeyCode.E)) {   
            target.SetActive(!target.activeSelf);
            activeState.SetActive(!activeState.activeSelf);
            gameObject.SetActive(!gameObject.activeSelf);
            leverActive = !leverActive;
            if (lightingPath != null){
                lightingPath.toggle();
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
