﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interacttype;
    private bool pickedup = false;
    private GameObject pickupparent;
    private bool eventhappened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (pickedup)
        {
            this.transform.position = pickupparent.transform.position + pickupparent.transform.forward * 1.2f;
            this.GetComponent<CharControl>().resetVelocity();
            this.transform.forward = pickupparent.transform.forward;
        }
    }

    public void Event()
    {

    }

    public void pickUp(GameObject player)
    {
        CharacterController ch = transform.GetComponent<CharacterController>();
        CapsuleCollider cl = transform.GetComponent<CapsuleCollider>();
        if (ch != null)
        {
            ch.Move(new Vector3(0, 0, 0));
            ch.enabled = !ch.enabled;
        }
        if ( cl != null)
        {
            cl.enabled = !cl.enabled;
        }
        pickedup = !pickedup;
        pickupparent = player;
    }
}
