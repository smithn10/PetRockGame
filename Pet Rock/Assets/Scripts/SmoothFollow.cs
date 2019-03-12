using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public GameObject followTarget;
    public float moveSpeed = 1;
    private Transform targetTransform;
    // Start is called before the first frame update 
    void Start()
    {
        UpdateTarget(followTarget);
    }

    public void UpdateTarget(GameObject target)
    {
        targetTransform = target.transform;
    }

    // Update is called once per frame 
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * moveSpeed);
    }
}