using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndGateOpener : MonoBehaviour {
    public GameObject player;
    public GameObject rock;
    public GameObject endGate;
    public int enemyCount;

    // Update is called once per frame
    void Update() {
        if(enemyCount <= 0) {
            endGate.GetComponent<Collider>().enabled = false;
            endGate.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void DecreaseCount() { enemyCount--; }
}
