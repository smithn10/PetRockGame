using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
    public GameObject rock;
    public GameObject player;
    public GameObject cam;
    public GameObject connectionObject;
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
