using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntAI : MonoBehaviour
{
    private Animator anim;
    private Transform path;
    private int currpath = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInChildren<Animator>();
        path = transform.parent.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(path.GetChild(currpath).position, transform.position) < .1)
        {
            currpath++;
            if (currpath == path.childCount)
                currpath = 0;
        }
        Vector3 movevec = path.GetChild(currpath).position - transform.position;
        movevec = movevec.normalized;
        transform.position += movevec * Time.deltaTime *.15f;
        transform.forward = Vector3.RotateTowards(transform.forward, movevec, 7 * Time.deltaTime, 0);
    }

    
}
