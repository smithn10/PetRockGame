using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignConversation : MonoBehaviour {
    public TextAsset file;
    public GameManager gameManager;
    public GameObject cam;
    public GameObject player;
    public GameObject textBox;
    public Text textInBox;
    private DialogueManager dialogueManager;
    private bool inRange = false;
    private bool isReading = false;

    void Start() {
        dialogueManager = gameManager.GetComponent<DialogueManager>(); // get dialogue manager
    }

    void Update() {
        if (inRange) {
            if ((isReading) || cam.GetComponent<SmoothFollow>().followTarget != player) {
                textBox.SetActive(false); // disable if player is reading or if player is not the boy character
            } else {
                textBox.SetActive(true); // display if player is boy character and not reading
            }

            if (Input.GetKeyDown(KeyCode.E)) { // in range of NPC and interact button was pressed
                isReading = true;
                dialogueManager.Reload(file); // reload function described in DialogueManager script
                dialogueManager.currLineIndex = 0;
                dialogueManager.EnableTextBox();
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.name == "Character") { // player is within range
            inRange = true; // update in range when entering a sign's trigger range
            isReading = false;
            textInBox.text = "Press [E] to read the sign";
        }
    }

    void OnTriggerExit(Collider col) {
        inRange = false; // update in range when leaving a sign's trigger range
        textBox.SetActive(false);
    }
}