using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public float accel = 10;
    public float decel = 5;
    public float maxspeed = 2;
    public float jumpheight = 3;
    public float gravity = -1;
    public Camera cam;
    private Vector3 movevec = new Vector3(0, 0, 0);
    private bool jumpbool = false;
    private Vector2 velocity = new Vector3(0, 0, 0);
    private CharacterController control;
    // Start is called before the first frame update
    void Start()
    {
        control = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (control.isGrounded && velocity.y < 0)
            velocity.y = 0;
        velocity.y += gravity * Time.deltaTime;
        movevec = camRelative(movevec);
        control.Move(movevec * Time.deltaTime * maxspeed);
        control.Move(velocity);
        if (movevec != Vector3.zero)
            transform.forward = movevec;
        if (jumpbool)
            Jump();
        movevec = new Vector3(0, 0, 0);
        jumpbool = false;
    }

    public void SetInput(float horizontal, float vertical, bool jumping)
    {
        movevec = new Vector3(horizontal, 0, vertical);
        jumpbool = jumping;
    }
    Vector3 camRelative(Vector3 vec)
    {
        return Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * vec;
    }
    void Jump()
    {
        velocity.y += Mathf.Sqrt(jumpheight * -.2f * gravity);
    }
    void MoveInstant(Vector3 vec)
    {
        transform.Translate(vec);
    }
}
