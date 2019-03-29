using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMovingBlocks : MonoBehaviour {
    public GameObject rock;
    public GameObject player;
    public GameObject cam;
    public GameObject[] connectionObjects;
    public GameObject activeState;
    public GameObject textBox;
    public Text textInBox;
    public float speed = 0f;
    public float cooldown = 0;
    public float Distance = 6f;
    public bool translateUp = true;
    private float DistCovered = 0f;
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
            if (DistCovered < Distance) {
                for (int j = 0; j < connectionObjects.Length; j++) {
                    float thisMoveDist = Mathf.Min(speed * Time.deltaTime, Distance - DistCovered);
                    if (translateUp) { connectionObjects[j].transform.Translate(Vector3.up * thisMoveDist); }
                    else { connectionObjects[j].transform.Translate(Vector3.down * thisMoveDist); }
                    DistCovered += thisMoveDist;
                }
            }

            if (DistCovered >= Distance) {
                translateUp = !translateUp;
                DistCovered = 0;
                cooldown = 120;
            }
        } else if ((leverActive) && (cooldown >= 1)) {
            cooldown--;
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.name == "Character") { // player collided with object
            inRange = true; // update in range when entering lever trigger range
            textBox.SetActive(true);
            textInBox.text = "Press [E] to use lever";
        }
    }

    void OnTriggerExit(Collider col) {
        inRange = false; // update in range when leaving lever trigger range
        textBox.SetActive(false);
    }
}
