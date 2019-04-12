using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// place script on NPC and invisible box colliders to create conversations
public class ActivateConversation : MonoBehaviour {
    public TextAsset file;
    public GameManager gameManager;
    public GameObject cam;
    public bool interactable;
    public GameObject player;
    private DialogueManager dialogueManager;
    private bool inRange = false;

    void Start() {
        dialogueManager = gameManager.GetComponent<DialogueManager>(); // get dialogue manager
    }

    void Update() {
        if (inRange) {
            if (Input.GetKeyDown(KeyCode.E)) { // in range of NPC and interact button was pressed
                dialogueManager.Reload(file); // reload function described in DialogueManager script
                dialogueManager.currLineIndex = 0;
                dialogueManager.EnableTextBox();
                //player.transform.LookAt(transform); // make player look at the NPC
                //transform.LookAt(player.transform); // make NPC look at the player
                //cam.transform.LookAt(transform); // focus camera on NPC
            }
        }
    }

    void OnTriggerEnter(Collider col) { 
        if(col.name == "Character") { // player collided with object
            if(interactable) { // interactable so update in range
                inRange = true; // update in range when entering an NPCs trigger range
            } else { // not interactable so automatically enable text box and dialogue
                dialogueManager.Reload(file); // reload function desrcibed in DialogueManager script
                dialogueManager.currLineIndex = 0;
                dialogueManager.EnableTextBox();
                Destroy(gameObject); // destory object because it is a one time pop up, so not an interactable
            }
        }
    }

    void OnTriggerExit(Collider col) {
        inRange = false; // update in range when leaving an NPCs trigger range
    }
}
