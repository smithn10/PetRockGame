using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    CharControl currChar;
    int currIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currChar = transform.GetChild(0).GetComponent<CharControl>();
    }
    
    void changeChar()
    {
        currIndex++;
        if (currIndex > transform.childCount-1)
            currIndex = 0;
        currChar = transform.GetChild(currIndex).GetComponent<CharControl>();
    }

    // Update is called once per frame
    void Update()
    {
        currChar.SetInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"));
        if (Input.GetButtonDown("ChangeChar"))
            changeChar();
    }
}
