using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelLever : MonoBehaviour {
    public GameObject cam;
    public GameObject rock;
    public GameObject ladderBlock;
    public GameObject rockPlatform;
    public GameObject activeState;
    public GameObject textBox;
    public GameManager manager;
    public Text textInBox;
    public Wiring lightingPath = null;
    public float speed = 0f;
    public float Distance = 6f;
    public bool rockInPlace;
    public bool leverActive;
    private float DistCovered = 0f;
    private bool inRange = false;

    void Update() {
        if ((inRange) && (Input.GetKeyDown(KeyCode.E)) && (!leverActive)) {
            activeState.SetActive(!activeState.activeSelf);
            gameObject.SetActive(!gameObject.activeSelf);
            manager.gameEnded = true;
            leverActive = !leverActive;
            if (lightingPath != null) {
                lightingPath.toggle();
            }
        }

        if(leverActive) {
            float thisMoveDist = Mathf.Min(speed * Time.deltaTime, Distance - DistCovered);
            rockPlatform.transform.Translate(Vector3.up * thisMoveDist);
            ladderBlock.transform.Translate(Vector3.up * thisMoveDist);
            DistCovered += thisMoveDist;
            textInBox.text = "";
        }
    }


    void OnTriggerEnter(Collider col) {
        if ((col.name == "Character") && (rockInPlace)) { // player collided with object
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