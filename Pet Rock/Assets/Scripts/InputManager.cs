using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    CharControl currChar;
    public GameObject player1;
    public GameObject player2;
    public bool lockMovement;
    int currIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currChar = player1.GetComponent<CharControl>();
    }
    
    public void changeChar()
    {
        currIndex++;
        currChar.SetInput(0, 0, false);
        if (currIndex > 1)
        {
            currIndex = 0;
            currChar = player1.GetComponent<CharControl>();
        }else
            currChar = player2.GetComponent<CharControl>();
    }

    // Update is called once per frame
    public void SendInput(float horizontal, float vertical, bool jump, bool interact)
    {
        if (lockMovement) { currChar.SetInput(0, 0, false); return; } // dont allow the player to move if the movement is locked

        currChar.SetInput(horizontal, vertical, jump);
        if (interact)
            currChar.Interact();
    }
    public void DisableFollow()
    {
        //player2.SendMessage("DisableFollow");
    }
}
