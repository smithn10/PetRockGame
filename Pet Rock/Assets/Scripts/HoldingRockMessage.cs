﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingRockMessage : MonoBehaviour {
    public GameObject rock;
    public GameObject textBox;
    public CharControl playerControllerScript;
    public SmoothFollow smoothCamScript;
    public Text textInBox;
    
    void Update() {
        if (playerControllerScript.IsPlayerHolding()) {
            if (smoothCamScript.followTarget == rock) {
                Debug.Log("Display message");
                textBox.SetActive(true);
                textInBox.text = "Can't play as rock while being held";
            } else {
                Debug.Log("No, " + smoothCamScript.followTarget + " " + playerControllerScript.IsPlayerHolding());
                textBox.SetActive(false);
            }
        }
    }
}
