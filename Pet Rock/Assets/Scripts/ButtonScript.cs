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
    public CharControl playerControllerScript;
    public Text textInBox;
    public Wiring lightingPath = null;
    public AudioSource sfx;
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
            if (lightingPath != null)
            {
                lightingPath.turnOn();
            }
        }else if (lightingPath != null)
        {
            lightingPath.turnOff();
        }

        if (cam.GetComponent<SmoothFollow>().followTarget == rock && !playerControllerScript.IsPlayerHolding()) { // check to see if character was switched to rock after trigger was enter
            textBox.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject == rock || (col.gameObject.tag == "Player" && playerControllerScript.IsPlayerHolding())) {
            onButton = true;
            textBox.SetActive(false);
            sfx.Play();
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
