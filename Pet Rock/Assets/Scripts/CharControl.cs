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
    public bool glideEnabled = false;
    public float glideFallSpeed = -1.5f;
    private float gravitystore = 0;
    public Camera cam;
    private Vector3 movevec = new Vector3(0, 0, 0);
    private bool jumpbool = false;
    private CharacterController control;
    private Vector3 chVelocity = new Vector3(0,0,0);
    public float interactDistance = 1;
    public GameObject rock;
    public GameObject gameManager;

    private Transform attachedobjectmin;
    private Transform attachedobjectmax;
    private string state = "neutral";
    private bool holdingSomething = false;
    private Interactable helditem;
    public float throwPower = .05f;
    private int i = 0;
    private CharacterController capsule;
    private bool spaceHeld = false;
    // Start is called before the first frame update 
    void Start()
    {
        control = transform.GetComponent<CharacterController>();
        gravitystore = gravity;
        capsule = transform.GetComponent<CharacterController>();
    }


    // Update is called once per frame 
    void Update()
    {
        if(glideEnabled) SendMessage("Gliding", false);
        SendMessage("SetAnimSpeed", movevec.magnitude);
        if (state == "ladder")
        {
            chVelocity = new Vector3(0, 0, 0);
            transform.position += new Vector3(0, movevec.z, 0) * Time.deltaTime * maxspeed;
            if (movevec.z != 0) { SendMessage("Climbing", true); SendMessage("PauseAnim", false); }
            else { SendMessage("PauseAnim", true); }
            if (transform.position.y > attachedobjectmax.position.y)
            {
                SendMessage("Climbing", false);
                state = "neutral";
                transform.position = new Vector3(transform.position.x, attachedobjectmax.position.y, transform.position.z);
                transform.position += transform.forward.normalized / 4;
            }
            if (transform.position.y < attachedobjectmin.position.y)
            {
                SendMessage("Climbing", false);
                state = "neutral";
                transform.position = new Vector3(transform.position.x, attachedobjectmin.position.y, transform.position.z);
                transform.position -= transform.forward.normalized / 4;
            }
            //rock.SendMessage("DisableFollow");
        }
        else if (state == "gliding")
        {
            SendMessage("Gliding", true);
            chVelocity = new Vector3(chVelocity.x, glideFallSpeed, chVelocity.z);
            Vector3 forward = chVelocity;
            forward.y = 0;
            //decelleration 
            if (forward.magnitude > 0)
            {
                float changemag = Mathf.Max(0, forward.magnitude - decel * Time.deltaTime) / forward.magnitude;
                forward = new Vector3(forward.x * changemag, 0, forward.z * changemag);
            }
            //acceleration 
            if (forward.magnitude < 4 * accel * Time.deltaTime)
            {
                forward += (movevec * 4 * accel * Time.deltaTime);
            }
            else
            {
                forward += (movevec * accel * Time.deltaTime);
            }
            if (forward.magnitude > 0.2)
                transform.forward = Vector3.RotateTowards(transform.forward, forward, 7 * Time.deltaTime, 0);

            //TODO: add return to normal with jumpup
            if (control.isGrounded || jumpbool)
            {
                state = "neutral";
            }else if (!spaceHeld)
            {

                state = "neutral";
            }

            //limiting speed 

            if (forward.magnitude > maxspeed)
            {
                forward = Vector3.ClampMagnitude(forward, maxspeed);
            }
            chVelocity = new Vector3(forward.x, chVelocity.y, forward.z);
            jumpbool = false;
            if (control.enabled)
                control.Move(chVelocity * Time.deltaTime);
        }
        else
        {
            chVelocity += new Vector3(0, gravity * 1.4f * Time.deltaTime, 0);
            if (chVelocity.y < -.1)
                chVelocity += new Vector3(0, gravity * .3f * Time.deltaTime, 0);
            if (spaceHeld)
                chVelocity += new Vector3(0, gravity * -.6f * Time.deltaTime, 0);
            Vector3 forward = chVelocity;
            forward.y = 0;
            //decelleration 
            if (forward.magnitude > 0)
            {
                float changemag = Mathf.Max(0, forward.magnitude - decel * Time.deltaTime) / forward.magnitude;
                forward = new Vector3(forward.x * changemag, 0, forward.z * changemag);
            }
            //acceleration 
            if (forward.magnitude < 4*accel*Time.deltaTime)
            {
                forward += (movevec * 4 * accel * Time.deltaTime);
            }
            else
            {
                forward += (movevec * accel * Time.deltaTime);
            }

            if (forward.magnitude > 0.2)
                transform.forward = Vector3.RotateTowards(transform.forward, forward, 7 * Time.deltaTime, 0);

            CheckSquish();
            //jumping, tests if the collider is grounded 
            if (control.isGrounded)
            {
                SendMessage("Landing", true);
                SendMessage("Falling", false);
                // SendMessage("Jumping", false);
                //here if is grounded 
                chVelocity = new Vector3(chVelocity.x, -.1f, chVelocity.z);
                if (jumpbool)
                {
                    // SendMessage("Jumping", true);
                    SendMessage("Landing", false);
                    Jump();
                }
            }
            else if (jumpbool && transform.tag == "Rock" && glideEnabled)
            {
                state = "gliding";
            }
            else
            {
                SendMessage("Landing", false);
                SendMessage("Falling", true);
                // SendMessage("Jumping", false);
            }

            //limiting speed 

            if (forward.magnitude > maxspeed)
            {
                forward = Vector3.ClampMagnitude(forward, maxspeed);
            }
            chVelocity = new Vector3(forward.x, chVelocity.y, forward.z);
            jumpbool = false;
            //if (this.gameObject.name == "Character") { SendMessage("Jumping", false); }
            if(control.enabled)
                control.Move(chVelocity*Time.deltaTime);
        }
        if (gameObject.tag == "Player" && holdingSomething && Input.GetMouseButtonDown(0))
        { // smash attack 
            helditem.pickUp(this.gameObject);
            holdingSomething = false;
            SendMessage("Holding", false);
            rock.SendMessage("Jump");
        }
    }
    //public method for recieveing input from inputhandler
    public void SetInput(float horizontal, float vertical, bool space)
    {
        movevec = new Vector3(horizontal, 0, vertical);
        movevec = camRelative(movevec);
        movevec = movevec.normalized;
        if(space && !spaceHeld)
        {
            jumpbool = true;
        }
        else
        {
            jumpbool = false;
        }
        spaceHeld = space;
    }
    //dont think this needs to be public
    public void LadderInteract(GameObject ladder)
    {
        if (state != "ladder" && gameObject.name == "Character")
        {
            attachedobjectmin = ladder.transform.parent.GetChild(0).transform;
            attachedobjectmax = ladder.transform.parent.GetChild(1).transform;
            transform.position = new Vector3(attachedobjectmax.position.x, transform.position.y, attachedobjectmax.position.z);
            if (transform.position.y > attachedobjectmax.position.y)
                transform.position = new Vector3(transform.position.x, attachedobjectmax.position.y, transform.position.z);
            if (transform.position.y < attachedobjectmin.position.y)
                transform.position = new Vector3(transform.position.x, attachedobjectmin.position.y, transform.position.z);
            transform.forward = attachedobjectmax.forward;
            state = "ladder";
            rock.SendMessage("DisableFollow");
            //SendMessage("Climbing", true);
        }
        else
        {
            SendMessage("PauseAnim", false);
            SendMessage("Falling", true);
            SendMessage("Climbing", false);
            state = "neutral";
        }

    }
    //unused
    public void resetVelocity()
    {
        chVelocity = new Vector3(0, 0, 0);
    }
    //used for applying force as a physics object(throwing)
    public void VelocityImpulse(Vector3 vector)
    {
        chVelocity += vector;
    }
    public void Interact()
    {
        if (state == "ladder")
        {
            SendMessage("PauseAnim", false);
            SendMessage("Falling", true);
            SendMessage("Climbing", false);
            state = "neutral";
            return;
        }
        if (holdingSomething)
        {
            helditem.pickUp(this.gameObject);
            holdingSomething = false;
            SendMessage("Holding", false);
            if (true)
            {
                helditem.GetComponent<CharControl>().Jump();
                helditem.GetComponent<CharControl>().VelocityImpulse(transform.forward * throwPower);
            }
            //rock.SendMessage("DisableFollow");
            return;
        }
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, interactDistance);
        Transform nearest = null;
        float nearDist = 9999;
        for (int i = 0; i < hitColliders.Length; i++)
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
        if (nearest != null)
        {
            Interactable inter = nearest.GetComponent<Interactable>();
            switch (inter.interacttype)
            {
                case "pickup":
                    holdingSomething = true;
                    SendMessage("Holding", true);
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
        chVelocity += new Vector3(0, Mathf.Sqrt(jumpheight * -.2f * gravitystore), 0);
        // SendMessage("Jumping", true);
    }
    void MoveInstant(Vector3 vec)
    {
        transform.Translate(vec);
    }
    private void CheckSquish()
    {
        Vector3 bottomSphere = this.transform.position - new Vector3(0, (capsule.height * transform.lossyScale.y) / 2 - capsule.radius * transform.lossyScale.y, 0);
        Collider[] hitColliders = Physics.OverlapSphere(bottomSphere + chVelocity * Time.deltaTime * 2, capsule.radius * transform.lossyScale.y);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (gameObject.tag == "Rock" && chVelocity.y < -5 && hitColliders[i].tag == "Enemy")
            {
                Debug.Log("Enemy Hit");
                Destroy(hitColliders[i].transform.parent.gameObject);
                gameManager.SendMessage("DecreaseCount");
            }
        }
    }
    void OnDrawGizmos()
    {
        //draw where it will be (about) next frame
        CharacterController capsule = this.GetComponent<CharacterController>();
        Vector3 bottomSphere = this.transform.position - new Vector3(0, (capsule.height*transform.lossyScale.y) / 2 - capsule.radius * transform.lossyScale.y, 0);
        Gizmos.DrawWireSphere(bottomSphere + chVelocity * Time.deltaTime * 2, capsule.radius*transform.lossyScale.y);
    }
    public bool IsPlayerHolding() { return holdingSomething; }
}   