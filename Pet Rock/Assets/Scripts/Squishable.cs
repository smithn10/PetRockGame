using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squishable : MonoBehaviour
{
    private bool squishing = false;
    GameObject rock;
    private Vector3 rockPos;
    private float rockHeight;
    public LevelEndGateOpener gameManager;
    public GameObject deathParticle;
    // Start is called before the first frame update
    void Start()
    {
        squishing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (squishing)
        {
            Transform childT = transform.GetChild(0).transform;
            BoxCollider boxC = GetComponent<BoxCollider>();
            rockPos = rock.transform.position;
            float squishspeed = .2f;
            float ratio = 1 - squishspeed;
            childT.localScale = new Vector3(childT.localScale.x, childT.localScale.y * ratio, childT.localScale.z);
            boxC.center = new Vector3(boxC.center.x, boxC.center.y - (boxC.size.y / 2) + (boxC.size.y / 2) * ratio, boxC.center.z);
            boxC.size = new Vector3(boxC.size.x, boxC.size.y * ratio, boxC.size.z);
            if (childT.localScale.y < 0.01f)
            {
                gameManager.DecreaseCount();
                if (deathParticle != null)
                    Instantiate(deathParticle, transform.position, Quaternion.Euler(-90, 0, 0));
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void DieInstant()
    {
        gameManager.DecreaseCount();
        if (deathParticle != null)
            Instantiate(deathParticle, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(transform.parent.gameObject);
    }

    public void StartSquishing(GameObject rck)
    {
        rock = rck;
        rockHeight = rck.transform.lossyScale.y * rck.GetComponent<CapsuleCollider>().radius + rck.transform.lossyScale.y * rck.GetComponent<CapsuleCollider>().height;
        squishing = true;
        rock.GetComponent<CharControl>().JumpFactor(3);
    }
}
