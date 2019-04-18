using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRockBite : MonoBehaviour {
    public FinalScript finalScript;
    public bool b;

    void OnEnable() {
        finalScript.RockBite(b);
    }
}
