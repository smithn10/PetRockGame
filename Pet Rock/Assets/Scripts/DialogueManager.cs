using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public GameObject textBox;
    public CharacterController playerController;
    public CharacterController rockController;
    public Text textInBox;
    public TextAsset file;
    public string[] lines;
    public int currLineIndex;
    public int maxLineIndex;
    public bool isEnabled = false;

    void Start() {
        if (file) { // make sure text file is not null
            lines = (file.text.Split('\n')); // makes lines array have each line of file
        }

        if(maxLineIndex == 0) { // if max line is set to 0 then use all the lines in the text file
            maxLineIndex = lines.Length - 1;
        }

        DisableTextBox();
    }

    void Update() {
        if(!isEnabled) { return; }

        textInBox.text = lines[currLineIndex]; // update the text in the box

        if(Input.GetKeyDown(KeyCode.Space)) { // allow the player to go through the text lines on key press
            currLineIndex++;
        }

        if(currLineIndex > maxLineIndex) { // if the dialogue is over then take the text box away
            DisableTextBox();
        }
    }

    public void EnableTextBox() {
        textBox.SetActive(true);
    }

    public void DisableTextBox() {
        textBox.SetActive(false);
    }
}

