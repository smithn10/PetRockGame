using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherInteractablesText : MonoBehaviour {
    public GameObject textBox;
    public Text textInBox;
    public string info;

    void OnTriggerEnter(Collider col) {
        if (col.name == "Character") { // player collided with object
            textBox.SetActive(true);
            textInBox.text = info;
        }
    }

    void OnTriggerExit(Collider col) {
        textBox.SetActive(false);
    }
}
