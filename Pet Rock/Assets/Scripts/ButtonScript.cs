using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
    public GameObject rock;
    public GameObject player;
    public GameObject cam;
    public GameObject connectionObject;
    public int ticks;
    public int i = 0;
    public float speed = 0f;
    private bool onButton = false;

    void Update() {
        if (onButton) {
            if (i < ticks) {
                connectionObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                i++;
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
