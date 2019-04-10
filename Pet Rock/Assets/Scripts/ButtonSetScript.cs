using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetScript : MonoBehaviour {
    public GameObject cam;
    public GameObject rock;
    public GameObject target;
    public GameObject counterpart;
    public GameObject textBox;
    public CharControl playerControllerScript;
    public Text textInBox;
    private bool onButton = false;
    private bool targetsActivated = false;
    private bool counterpartsActivated = false;

    void Update() {
        if (onButton) {
            targetsActivated = target.activeSelf;
            counterpartsActivated = counterpart.activeSelf;
            
            if(!targetsActivated) { // case that the switches targets are not yet active
                target.SetActive(!target.activeSelf); // activate them
            }

            if(counterpartsActivated) { // case that the switches counterparts are active
                counterpart.SetActive(!counterpart.activeSelf); // deactive them
            }

            onButton = false;
        }

        if(cam.GetComponent<SmoothFollow>().followTarget == rock && !playerControllerScript.IsPlayerHolding()) { // check to see if character was switched to rock after trigger was enter
            textBox.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col) {
        if ((col.gameObject == rock) || (col.gameObject.tag == "Player" && playerControllerScript.IsPlayerHolding())) {
            onButton = true;
            textBox.SetActive(false);
        } else {
            textBox.SetActive(true);
            textInBox.text = "Rock needed to activate";
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject == rock) {
            onButton = false;
        }
        textBox.SetActive(false);
    }
}
