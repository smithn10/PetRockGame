using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interacttype;
    private bool pickedup = false;
    private GameObject pickupparent;
    private bool eventhappened = false;

    private float width = 0;
    private Vector3 parentPos;
    // Start is called before the first frame update
    void Start()
    {
        width = transform.localScale.x * GetComponent<CapsuleCollider>().radius;
        parentPos = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (pickedup)
        {


            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 9;

            RaycastHit hit;
            parentPos = pickupparent.transform.position;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(parentPos, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && hit.distance < 1.2 + width)
            {
                Debug.DrawRay(parentPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                this.transform.position = pickupparent.transform.position + pickupparent.transform.forward * (hit.distance-width);
                this.GetComponent<CharControl>().resetVelocity();
                this.transform.forward = pickupparent.transform.forward;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

                this.transform.position = pickupparent.transform.position + pickupparent.transform.forward * 1.2f;
                this.GetComponent<CharControl>().resetVelocity();
                this.transform.forward = pickupparent.transform.forward;
            }
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
            if(ch.enabled)
                ch.Move(new Vector3(0, 0, 0));
            ch.enabled = !ch.enabled;
            if (ch.enabled)
                ch.Move(new Vector3(0, 0, 0));
        }
        if ( cl != null)
        {
            cl.enabled = !cl.enabled;
        }
        pickedup = !pickedup;
        pickupparent = player;
    }
}
