using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour {
    public GameObject[] objects;

    void Start() {
        for (int i = 0; i < objects.Length; i++) {
            Destroy(objects[i]);
        }
    }
}
