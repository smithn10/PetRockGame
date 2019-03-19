using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfMap : MonoBehaviour {
    public GameObject player;
    public GameObject rock;
    public string currentLevel;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player || other.gameObject == rock) {
            SceneManager.LoadScene(currentLevel);
        }
    }
}
