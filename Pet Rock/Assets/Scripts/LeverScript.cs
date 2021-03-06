﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverScript : MonoBehaviour {
    public GameObject cam;
    public GameObject target;
    public GameObject activeState;
    public GameObject textBox;
    public Text textInBox;
    public Animator animator;
    public Wiring lightingPath = null;
    public AudioSource sfx;
    public bool canBeSwitchedBack = true;
    public bool rockCanSwitch = false;
    public bool bridgeConnector = false;
    private bool inRange = false;
    private bool leverActive = false;
    private bool isPlayer = true;

    void Update() {
        //toggle active state of stairs, lever, and other lever
        if (inRange && Input.GetKeyDown(KeyCode.E)) { 
            if(!isPlayer) { animator.Play("RockTentacleSwitch"); }
            if(bridgeConnector) { sfx.Play(); }

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
        if (((col.name == "Character") || ((col.name == "Rock") && (rockCanSwitch))) && (canBeSwitchedBack)) { // player collided with object
            inRange = true; // update in range when entering lever trigger range
            textBox.SetActive(true);
            textInBox.text = "Press [E] to use lever";
            if(col.name == "Rock") { isPlayer = false; }
            else if (col.name == "Character") { isPlayer = true; }
        } else if (!canBeSwitchedBack) { textBox.SetActive(false); }
    }

    void OnTriggerExit(Collider col) {
        inRange = false; // update in range when leaving lever trigger range
        textBox.SetActive(false);
    }
}
