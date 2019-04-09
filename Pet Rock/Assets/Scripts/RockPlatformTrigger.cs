using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPlatformTrigger : MonoBehaviour {
    public GameObject rock;
    public EndLevelLever leverActive;
    public EndLevelLever leverInactive;

    void OnTriggerEnter(Collider col) {
        if (col.gameObject == rock) {
            leverActive.rockInPlace = true;
            leverInactive.rockInPlace = true;
            Debug.Log("Rock, update rockinplace");
        } else { Debug.Log("NOT ROCK"); }
        
    }

    void OnTriggerExit(Collider other) {
        leverActive.rockInPlace = false;
        leverInactive.rockInPlace = false;
        Debug.Log("Exit trigger");
    }
}
