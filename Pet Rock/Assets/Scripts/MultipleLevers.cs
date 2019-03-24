using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLevers : MonoBehaviour {
    public GameObject[] levers;
    public GameObject connectionObject;
    private bool allOn = false;

    void Update() {
        for(int i=0; i<levers.Length; i++) {
            if(!levers[i].activeSelf) {
                allOn = false;
                break;
            } else {
                allOn = true;
            }
        }

        if(allOn) {
            connectionObject.SetActive(true);
        } else {
            connectionObject.SetActive(false);
        }
    }
}
