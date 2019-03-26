using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject inputManager;
    public GameObject cam;
    public bool canChangeChar = false;
    private bool activePlayer = true;
    private bool paused = false;
    private InputManager inpMangr;

    private void Start()
    {
        inpMangr = inputManager.GetComponent<InputManager>();
    }

    void Update() {
        if((Input.GetKeyDown(KeyCode.Tab)) && (!paused)) {
            if(activePlayer) { // switch to rock
                cam.SendMessage("FocusRock");
                inpMangr.DisableFollow();
                activePlayer = false;
            } else { // switch to player
                cam.SendMessage("FocusPlayer");
                activePlayer = true;
            }
            inpMangr.changeChar();
        }

        if (!paused && !(!canChangeChar && !activePlayer))
        {
            inpMangr.SendInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetButton("Jump"), Input.GetButtonDown("Interact"));
        }

        if(Input.GetKeyDown(KeyCode.Escape)) { // check for when we add in the pause feature
            if(paused) { paused = false; }
            else { paused = true; }
        }
        
    }

    void AllowChangeChar() { canChangeChar = true; }
}
