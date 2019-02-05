using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
    public GameObject rock;
    public GameObject player;
    public GameObject cam;
    public GameObject connectionObject;
    private float speed = 0f;

    void Update() {
        connectionObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject == rock) {
            speed = 2f;
            //cam.SendMessage("FocusPlayer");
            rock.SendMessage("DisableFollow");
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject == rock) {
            speed = 0f;
            rock.SendMessage("EnableFollow");
        }
    }
}
