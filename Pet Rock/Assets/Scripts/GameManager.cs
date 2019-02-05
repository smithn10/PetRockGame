using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject rock;
    public GameObject cam;
    private bool activePlayer = true;
    private bool paused = false;

    void Start() {
        
    }

    void Update() {
        if((Input.GetKeyDown(KeyCode.Tab)) && (!paused)) {
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
}
