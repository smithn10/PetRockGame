using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour {
    public LevelEndGateOpener gateOpenerScript;
    public int counter;
    public Text myText;

    void Update() {
        counter = gateOpenerScript.enemyCount;
        if (counter > 0) {
            myText.text = "Enemies left: " + counter;
        } else {
            myText.text = "LEVEL COMPLETE";
        }
    }
}
