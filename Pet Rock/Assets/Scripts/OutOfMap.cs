using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMap : MonoBehaviour {
    public GameObject player;
    public GameObject rock;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player || other.gameObject == rock) {
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
