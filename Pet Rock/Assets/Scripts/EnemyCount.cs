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
        myText.text = "Enemies left: " + counter;
    }
}
