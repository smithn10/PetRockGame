using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public GameObject followTarget;
    public float smoothTime = 1;
    private Transform targetTransform;
    private Vector3 velocity = Vector3.zero;
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
        transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position, ref velocity, smoothTime);
    }
}
