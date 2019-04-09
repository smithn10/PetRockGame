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
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == rock) {
            leverActive.rockInPlace = false;
            leverInactive.rockInPlace = false;
        }
    }
}
