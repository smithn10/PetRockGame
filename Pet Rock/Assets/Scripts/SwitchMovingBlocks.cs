using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMovingBlocks : MonoBehaviour {
    public GameObject rock;
    public GameObject player;
    public GameObject cam;
    public GameObject[] connectionObjects;
    public GameObject activeState;
    public int ticks;
    public int i = 0;
    public float speed = 0f;
    public float cooldown = 0;
    public bool translateUp = true;
    private bool inRange = false;
    public bool leverActive = false;

    void Update() {
        if (inRange) {
            if ((Input.GetKeyDown(KeyCode.E)) && (!leverActive)) {
                activeState.SetActive(!activeState.activeSelf);
                gameObject.SetActive(!gameObject.activeSelf);
            } else if ((Input.GetKeyDown(KeyCode.E)) && (leverActive)) {
                activeState.SetActive(!activeState.activeSelf);
                gameObject.SetActive(!gameObject.activeSelf);
            }
        }

        if ((leverActive) && (cooldown < 1)) {
            if (i < ticks) {
                for (int j = 0; j < connectionObjects.Length; j++) {
                    if (translateUp) { connectionObjects[j].transform.Translate(Vector3.up * speed * Time.deltaTime); }
                    else { connectionObjects[j].transform.Translate(Vector3.down * speed * Time.deltaTime); }
                    i++;
                }
            }

            if (i >= ticks) {
                translateUp = !translateUp;
                i = 0;
                cooldown = 120;
            }
        } else if ((leverActive) && (cooldown >= 1)) {
            cooldown--;
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
