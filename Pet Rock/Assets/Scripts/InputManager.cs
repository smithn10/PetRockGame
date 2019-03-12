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
    
    void changeChar()
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
    void Update()
    {
        if (lockMovement) { return; } // dont allow the player to move if the movement is locked

        currChar.SetInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"));
        if (Input.GetButtonDown("ChangeChar"))
            changeChar();
        if (Input.GetButtonDown("Interact"))
            currChar.Interact();
    }
}
