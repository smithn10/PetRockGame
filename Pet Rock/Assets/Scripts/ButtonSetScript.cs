using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetScript : MonoBehaviour {
    public GameObject rock;
    public GameObject target;
    public GameObject counterpart;
    public GameObject textBox;
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
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject == rock) {
            onButton = true;
            textBox.SetActive(false);
        } else {
            textBox.SetActive(true);
            textInBox.text = "Not heavy enough to activate";
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject == rock) {
            onButton = false;
        }
        textBox.SetActive(false);
    }
}
