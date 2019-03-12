using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public GameObject textBox;
    public GameObject players;
    public GameObject cam;
    public Text textInBox;
    public TextAsset file;
    public string[] lines;
    public int currLineIndex;
    public int maxLineIndex;
    public bool isEnabled;
    private InputManager inputController;
    private CameraOrbit cameraScript;


    void Start() {
        inputController = players.GetComponent<InputManager>(); // get input manager script for movement control
        cameraScript = cam.GetComponent<CameraOrbit>(); // get camera follow script for orbit control

        if (file) { // make sure text file is not null
            lines = (file.text.Split('\n')); // makes lines array have each line of file
        }

        maxLineIndex = lines.Length - 1; // update the max lines

        if (isEnabled) { EnableTextBox(); }
        else { DisableTextBox(); }
    }

    void Update() {
        if(!isEnabled) { return; } // if text box is not enabled then don't update

        textInBox.text = lines[currLineIndex]; // update the text in the box

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) { // allow the player to go through the text lines on key press
            currLineIndex++;
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            currLineIndex = maxLineIndex + 1;
        }

        if(currLineIndex > maxLineIndex) { // if the dialogue is over then take the text box away
            DisableTextBox();
        }
    }

    public void EnableTextBox() { // function to enable the text box and disable player movement
        textBox.SetActive(true);
        isEnabled = true;
        inputController.lockMovement = true; // lock player movement
        cameraScript.lockOrbit = true; // lock camera orbit
    }

    public void DisableTextBox() { // function to disable text box and enable player movement
        textBox.SetActive(false);
        isEnabled = false;
        inputController.lockMovement = false; // unlock player movement
        cameraScript.lockOrbit = false; // unlock camera orbit
    }

    public void Reload(TextAsset newFile) { // function to load in a new text file for other dialogues
        if(newFile != null) { // make sure the text asset being passed in is not null
            string[] newLines = (newFile.text.Split('\n')); // split the new file into lines
            lines = newLines; // update lines to be new lines and avoid aliasing
            maxLineIndex = lines.Length - 1; // update the max lines
        }
    }
}

