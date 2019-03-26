using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiring : MonoBehaviour
{
    public Material litMat;
    private Material notMat;
    public Wiring next = null;
    private bool on = false;

    public bool andGate = false;
    public int numInputs = 0;
    private int onInpts = 0;
    // Start is called before the first frame update
    void Start()
    {
        notMat = litMat;
    }

    public void toggle()
    {
        if (on)
        {
            turnOff();
        }
        else
        {
            turnOn();
        }
    }

    public void turnOn()
    {
        if (andGate)
            onInpts++;
        if (!andGate && !on || !on && onInpts >= numInputs)
        {
            Material temp = notMat;
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                temp = r.material;
                r.material = notMat;
            }
            notMat = temp;
            if (next != null && next != this)
            {
                next.turnOn();
            }
            on = true;
        }
    }
    public void turnOff()
    {
        if (andGate)
            onInpts--;
        if (!andGate && on || on && onInpts < numInputs)
        {
            Material temp = notMat;
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                temp = r.material;
                r.material = notMat;
            }
            notMat = temp;
            if (next != null && next != this)
            {
                next.turnOff();
            }
            on = false;
        }
    }
}
