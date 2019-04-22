using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicEnd : MonoBehaviour {
    void OnEnable() {
        SceneManager.LoadScene("Level0");
    }
}
