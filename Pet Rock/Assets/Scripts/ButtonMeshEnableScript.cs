using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMeshEnableScript : MonoBehaviour {
    public GameObject rock;
    public GameObject cam;
    public GameObject target;
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
            rock.SendMessage("DisableFollow");
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject == rock) {
            rock.SendMessage("EnableFollow");
        }
    }
}
