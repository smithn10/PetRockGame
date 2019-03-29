using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMeshEnableScript : MonoBehaviour {
    public GameObject rock;
    public GameObject cam;
    public GameObject target;
    public GameObject textBox;
    public Text textInBox;
    private bool onButton = false;
    private bool activated = false;

    void Update() {
        if ((onButton) && (!activated)) {
            target.SetActive(!target.activeSelf);
            activated = true;
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
            rock.SendMessage("EnableFollow");
        }
        textBox.SetActive(false);
    }
}
