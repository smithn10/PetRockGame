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
    private float gravitystore = 0;
    public Camera cam;
    private Vector3 movevec = new Vector3(0, 0, 0);
    private bool jumpbool = false;
    private Rigidbody control;
    public float interactDistance = 1;
    public GameObject rock;
    public GameObject gameManager;

    private Transform attachedobjectmin;
    private Transform attachedobjectmax;
    private bool onLadder = false;
    private bool holdingSomething = false;
    private Interactable helditem;
    public float throwPower = .05f;
    private int i = 0;
    private CapsuleCollider capsule;
    // Start is called before the first frame update 
    void Start()
    {
        control = transform.GetComponent<Rigidbody>();
        gravitystore = gravity;
        capsule = transform.GetComponent<CapsuleCollider>();
    }


    // FixedUpdate is called once per physicsUpdate 
    void FixedUpdate()
    {
        if (onLadder)
        {
            control.velocity = new Vector3(0, 0, 0);
            transform.position += new Vector3(0, movevec.z, 0) * Time.fixedDeltaTime * maxspeed;
            if (transform.position.y > attachedobjectmax.position.y)
            {
                onLadder = false;
                transform.position = new Vector3(transform.position.x, attachedobjectmax.position.y, transform.position.z);
                transform.position += transform.forward.normalized / 4;
            }
            if (transform.position.y < attachedobjectmin.position.y)
            {
                onLadder = false;
                transform.position = new Vector3(transform.position.x, attachedobjectmin.position.y, transform.position.z);
                transform.position -= transform.forward.normalized / 4;
            }
            rock.SendMessage("DisableFollow");
        }
        else
        {
            control.velocity += new Vector3(0, gravity * Time.fixedDeltaTime, 0);
            Vector3 forward = control.velocity;
            forward.y = 0;
            //decelleration 
            if (forward.magnitude > 0)
            {
                float changemag = Mathf.Max(0, forward.magnitude - decel * Time.fixedDeltaTime) / forward.magnitude;
                forward = new Vector3(forward.x * changemag, 0, forward.z * changemag);
            }
            //acceleration 
            if (forward.magnitude < 4*accel*Time.fixedDeltaTime)
            {
                forward += (movevec.normalized * 4 * accel * Time.fixedDeltaTime);
            }
            else
            {
                forward += (movevec.normalized * accel * Time.fixedDeltaTime);
            }

            if (forward.magnitude > 0.2)
                transform.forward = Vector3.RotateTowards(transform.forward, forward, 7 * Time.fixedDeltaTime, 0);

            //jumping, tests if the collider is grounded 
            Vector3 bottomSphere = this.transform.position - new Vector3(0, (capsule.height * transform.lossyScale.y) / 2 - capsule.radius * transform.lossyScale.y, 0);
            bottomSphere += new Vector3(0, -.15f, 0);
            Collider[] hitColliders = Physics.OverlapSphere(bottomSphere + Vector3.down * (Time.fixedDeltaTime + .2f), capsule.radius * transform.lossyScale.y - .1f);
            int x = 0;
            for (int i = 0; i < hitColliders.Length; i++)
                if (!hitColliders[i].isTrigger)
                    x++;
            if (x > 1)
            {
                //here if is grounded 
                gravity = 0;
                control.velocity = new Vector3(control.velocity.x, 0, control.velocity.z);
                Debug.Log(control.velocity);
                if (jumpbool)
                    Jump();
            }
            else
            {
                gravity = gravitystore;
            }

            //limiting speed 

            if (forward.magnitude > maxspeed)
            {
                forward = Vector3.ClampMagnitude(forward, maxspeed);
            }
            control.velocity = new Vector3(forward.x, control.velocity.y, forward.z);
            movevec = new Vector3(0, 0, 0);
            jumpbool = false;
        }
        if (gameObject.tag == "Player" && holdingSomething && Input.GetMouseButtonDown(0))
        { // smash attack 
            helditem.pickUp(this.gameObject);
            holdingSomething = false;
            rock.SendMessage("Jump");
        }
        CheckSquish();
    }
    //public method for recieveing input from inputhandler
    public void SetInput(float horizontal, float vertical, bool jumping)
    {
        movevec = new Vector3(horizontal, 0, vertical);
        movevec = camRelative(movevec);
        movevec = movevec.normalized;
        jumpbool = jumping;
    }
    //dont think this needs to be public
    public void LadderInteract(GameObject ladder)
    {
        if (!onLadder && gameObject.name == "Character")
        {
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
        control.velocity = new Vector3(0, 0, 0);
    }
    //used for applying force as a physics object(throwing)
    public void VelocityImpulse(Vector3 vector)
    {
        control.velocity += vector;
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
            if (true)
            {
                helditem.GetComponent<CharControl>().Jump();
                helditem.GetComponent<CharControl>().VelocityImpulse(transform.forward * throwPower);
            }
            rock.SendMessage("DisableFollow");
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
        control.velocity += new Vector3(0, Mathf.Sqrt(jumpheight * -.2f * gravitystore), 0);
    }
    void MoveInstant(Vector3 vec)
    {
        transform.Translate(vec);
    }
    private void CheckSquish()
    {
        Vector3 bottomSphere = this.transform.position - new Vector3(0, (capsule.height * transform.lossyScale.y) / 2 - capsule.radius * transform.lossyScale.y, 0);
        Collider[] hitColliders = Physics.OverlapSphere(bottomSphere + control.velocity * Time.fixedDeltaTime * 2, capsule.radius * transform.lossyScale.y);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (gameObject.tag == "Rock" && control.velocity.y < -5 && hitColliders[i].tag == "Enemy")
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
        CapsuleCollider capsule = this.GetComponent<CapsuleCollider>();
        Vector3 bottomSphere = this.transform.position - new Vector3(0, (capsule.height*transform.lossyScale.y) / 2 - capsule.radius * transform.lossyScale.y, 0);
        Gizmos.DrawWireSphere(bottomSphere + transform.GetComponent<Rigidbody>().velocity * Time.fixedDeltaTime * 2, capsule.radius*transform.lossyScale.y);
    }
}