using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleLevers : MonoBehaviour {
    public GameObject[] levers;
    public GameObject connectionObject;
    private bool allOn = false;

    void Update() {
        Debug.Log("Here");
        for(int i=0; i<levers.Length; i++) {
            if(!levers[i].activeSelf) {
                Debug.Log("One inactive: break");
                allOn = false;
                break;
            } else {
                allOn = true;
            }
        }

        if(allOn) {
            Debug.Log("ALL ON");
            connectionObject.SetActive(true);
        } else {
            Debug.Log("all off");
            connectionObject.SetActive(false);
        }
    }
}
