using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject rock;
    public GameObject cam;
    private bool activePlayer = true;

    void Start() {
        
    }
    

    void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
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
    }
}
