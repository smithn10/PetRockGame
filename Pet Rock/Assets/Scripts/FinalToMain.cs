using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalToMain : MonoBehaviour {

    void OnEnable() {
        SceneManager.LoadScene("Main Menu Scene");
    }
}
