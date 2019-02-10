using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
    public GameObject rock;
    public GameObject player;
    public GameObject cam;
    public GameObject connectionObject;
    public GameObject connector;
    public float speed = 0f;
    private bool onButton = false;
    private bool reachedDestination = false;

    void Update() {
        if (onButton) {
            //reachedDestination = connectionObject.GetComponent<Collider>().IsTouching
            while (!reachedDestination) {
                connectionObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            
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
            onButton = false;
            rock.SendMessage("EnableFollow");
        }
    }
}
