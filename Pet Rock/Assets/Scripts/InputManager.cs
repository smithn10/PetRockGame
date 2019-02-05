using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    CharControl currChar;
    public GameObject player1;
    public GameObject player2;
    int currIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currChar = player1.GetComponent<CharControl>();
    }
    
    void changeChar()
    {
        currIndex++;
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
        currChar.SetInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"));
        if (Input.GetButtonDown("ChangeChar"))
            changeChar();
    }
}
