using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public float accel = 10;
    public float decel = 5;
    public float maxspeed = 5;
    public float jumpheight = 0.5f;
    public float gravity = -1;
    public float aerialDrag = .2f;
    public Camera cam;
    private Vector3 movevec = new Vector3(0, 0, 0);
    private bool jumpbool = false;
    private Vector3 velocity = new Vector3(0, 0, 0);
    private CharacterController control;
    public float interactDistance = 1;
    public GameObject rock;
    public GameObject gameManager;

    private Transform attachedobjectmin;
    private Transform attachedobjectmax;
    private bool onLadder = false;
    private bool holdingSomething = false;
    private Interactable helditem;
    public float throwPower = .05f;
    // Start is called before the first frame update
    void Start()
    {
        control = transform.GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 ad = new Vector2(velocity.x, velocity.z);
        Vector2 adnorm = ad.normalized;
        float newx = Mathf.Sign(velocity.x) * Mathf.Max(0, Mathf.Abs(velocity.x) - Mathf.Abs(adnorm.x)*Time.deltaTime * aerialDrag);
        float newz = Mathf.Sign(velocity.z) * Mathf.Max(0, Mathf.Abs(velocity.z) - Mathf.Abs(adnorm.y)*Time.deltaTime * aerialDrag);
        velocity = new Vector3(newx, velocity.y, newz);
        if (onLadder)
        {
            velocity = new Vector3(0, 0, 0);
            control.Move(new Vector3(0, movevec.z, 0) * Time.deltaTime * maxspeed);
            if (transform.position.y > attachedobjectmax.position.y)
            {
                onLadder = false;
                transform.position = new Vector3(transform.position.x, attachedobjectmax.position.y, transform.position.z);
                transform.position += transform.forward/4;
            }
            if (transform.position.y < attachedobjectmin.position.y)
            {
                onLadder = false;
                transform.position = new Vector3(transform.position.x, attachedobjectmin.position.y, transform.position.z);
                transform.position -= transform.forward/4;
            }
            rock.SendMessage("DisableFollow");
        }
        else
        {
            if (control.isGrounded && velocity.y < 0)
                velocity.y = 0;
            velocity.y += gravity * Time.deltaTime;
            movevec = camRelative(movevec);
            control.Move(movevec * Time.deltaTime * maxspeed);
            control.Move(velocity);
            if (movevec.magnitude > 0.2)
                transform.forward = Vector3.RotateTowards(transform.forward, movevec, 7*Time.deltaTime, 0);
            if (jumpbool && control.isGrounded)
                Jump();
            movevec = new Vector3(0, 0, 0);
            jumpbool = false;
        }
        if(gameObject.tag == "Player" && holdingSomething && Input.GetMouseButtonDown(0))
        {
            helditem.pickUp(this.gameObject);
            holdingSomething = false;
            rock.SendMessage("Jump");
            Debug.Log("yup");
        }
    }
    //public method for recieveing input from inputhandler
    public void SetInput(float horizontal, float vertical, bool jumping)
    {
        movevec = new Vector3(horizontal, 0, vertical);
        jumpbool = jumping;
    }
    //dont think this needs to be public
    public void LadderInteract(GameObject ladder)
    {
        if (!onLadder) {
            attachedobjectmin = ladder.transform.parent.GetChild(0).transform;
            attachedobjectmax = ladder.transform.parent.GetChild(1).transform;
            transform.position = new Vector3(attachedobjectmax.position.x, transform.position.y, attachedobjectmax.position.z);
            if (transform.position.y > attachedobjectmax.position.y)
                transform.position = new Vector3(transform.position.x, attachedobjectmax.position.y, transform.position.z);
            if (transform.position.y < attachedobjectmin.position.y)
                transform.position = new Vector3(transform.position.x, attachedobjectmin.position.y, transform.position.z);
            transform.forward = attachedobjectmax.forward;
            onLadder = true;
            rock.SendMessage("DisableFollow");
        }
        else
        {
            onLadder = false;
        }
        
    }
    //unused
    public void resetVelocity()
    {
        velocity = new Vector3(0, 0, 0);
    }
    //used for applying force as a physics object(throwing)
    public void VelocityImpulse(Vector3 vector)
    {
        velocity += vector;
    }
    public void Interact()
    {
        if (onLadder)
        {
            onLadder = false;
            return;
        }
        if (holdingSomething)
        {
            helditem.pickUp(this.gameObject);
            holdingSomething = false;
            if (!control.isGrounded)
            {
                helditem.GetComponent<CharControl>().Jump();
                helditem.GetComponent<CharControl>().VelocityImpulse(transform.forward*throwPower);
            }
            rock.SendMessage("DisableFollow");
            return;
        }
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, interactDistance);
        Transform nearest = null;
        float nearDist = 9999;
        for(int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.name != this.name && hitColliders[i].GetComponent<Interactable>() != null)
            {
                float thisDist = (transform.position - hitColliders[i].transform.position).sqrMagnitude;
                if (thisDist < nearDist)
                {
                    nearDist = thisDist;
                    nearest = hitColliders[i].transform;
                }
            }
        }
        if(nearest != null)
        {
            Interactable inter = nearest.GetComponent<Interactable>();
            switch (inter.interacttype)
            {
                case "pickup":
                    holdingSomething = true;
                    helditem = inter;
                    inter.pickUp(this.gameObject);
                    break;
                case "ladder":
                    LadderInteract(inter.gameObject);
                    break;
            }
        }
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
    //squish bug when vertical velocity above some point, requires proper tags
    void OnControllerColliderHit(ControllerColliderHit col)
    {
        if (gameObject.tag == "Rock" && velocity.y < -.02f)
            //Debug.Log(velocity.y);
        if (velocity.y < -.2 && gameObject.tag == "Rock" && col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            gameManager.SendMessage("DecreaseCount");
        }
    }
}
