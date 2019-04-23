using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAnimationEnable : MonoBehaviour {
    public FinalScript finalScript;
    public Animator anim;
    public string animationFunction;

    void OnEnable() {
        finalScript.SendMessage(animationFunction, true);
    }
}
