using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

    public void PlayGame() {
        SceneManager.LoadScene("Beginning Comic");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Credits() {
        SceneManager.LoadScene("Credits Scene");
    }

    public void Back() {
        SceneManager.LoadScene("Main Menu Scene");
    }
}
