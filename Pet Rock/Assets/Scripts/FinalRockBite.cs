using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRockBite : MonoBehaviour {
    public FinalScript finalScript;

    void OnEnable() {
        finalScript.RockBite(true);
        finalScript.RockBite(false);
    }
}
