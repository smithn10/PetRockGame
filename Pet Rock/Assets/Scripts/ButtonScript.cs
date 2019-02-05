using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
    public GameObject rock;
    public GameObject player;
    public GameObject cam;
    public GameObject connectionObject;

    void OnCollisionEnter(Collision col) {
        if (col.gameObject == rock) {
            Destroy(connectionObject);
        }
    }
}
