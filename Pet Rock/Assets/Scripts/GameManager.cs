using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject rock;
    public GameObject cam;
    public bool canChangeChar = false;
    private bool activePlayer = true;
    private bool paused = false;

    void Update() {
        if((Input.GetKeyDown(KeyCode.Tab)) && (!paused) && (canChangeChar)) {
            if(activePlayer) { // switch to rock
                cam.SendMessage("FocusRock");
                rock.SendMessage("DisableFollow");
                activePlayer = false;
            } else { // switch to player
                cam.SendMessage("FocusPlayer");
                rock.SendMessage("EnableFollow");
                activePlayer = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape)) { // check for when we add in the pause feature
            if(paused) { paused = false; }
            else { paused = true; }
        }
    }

    void AllowChangeChar() { canChangeChar = true; }
}
