using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
    public GameObject rock;
    public GameObject player;
    public GameObject cam;
    public GameObject connectionObject;
    public GameObject textBox;
    public Text textInBox;
    public float Distance = 6f;
    private float DistCovered = 0f;
    public float speed = 0f;
    private bool onButton = false;


    void Update() {
        if (onButton) {
            if (DistCovered < Distance) {
                float thisMoveDist = Mathf.Min(speed * Time.deltaTime, Distance - DistCovered);
                connectionObject.transform.Translate(Vector3.forward * thisMoveDist);
                DistCovered += thisMoveDist;
            }
        }

        if (cam.GetComponent<SmoothFollow>().followTarget == rock) { // check to see if character was switched to rock after trigger was enter
            textBox.SetActive(false);
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
