using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockTentacleSwitch : MonoBehaviour {
    public GameObject cam;
    public GameObject target;
    public GameObject activeState;
    public GameObject rock;
    public GameObject textBox;
    public Text textInBox;
    public float speed = 0f;
    public float Distance = 6f;
    public float angleThreshold;
    public bool leverActive;
    private float DistCovered = 0f;
    private bool inRange = false;

    void Update() {
        if (inRange) {
            Vector3 direction = (transform.position - rock.transform.position).normalized;
            float angle = Vector3.Dot(direction, rock.transform.forward);
            float distance = direction.magnitude;

            if((angle > angleThreshold) && (cam.GetComponent<SmoothFollow>().followTarget == rock)) {
                textBox.SetActive(true);
                textInBox.text = "Press [E] to use tentacle";

                if (Input.GetKeyDown(KeyCode.E)) {
                   activeState.SetActive(!activeState.activeSelf);
                   activeState.SendMessage("UpdateDistCovered", DistCovered);
                   gameObject.SetActive(!gameObject.activeSelf);
                }
            } else if ((angle <= angleThreshold) || (cam.GetComponent<SmoothFollow>().followTarget != rock)) {
                textBox.SetActive(false);
            }
        }
        
        if ((leverActive) && (DistCovered < Distance)) {
            float thisMoveDist = Mathf.Min(speed * Time.deltaTime, Distance - DistCovered);
            target.transform.Translate(Vector3.right * thisMoveDist);
            DistCovered += thisMoveDist;
        } else if ((!leverActive) && (DistCovered > 0)) {
            float thisMoveDist = Mathf.Min(speed * Time.deltaTime, DistCovered);
            target.transform.Translate(Vector3.left * thisMoveDist);
            DistCovered -= thisMoveDist;
        }
    }


    void OnTriggerEnter(Collider col) {
        if ((col.name == "Rock") && (cam.GetComponent<SmoothFollow>().followTarget == rock)) {
            inRange = true; // update in range when entering lever tentacle trigger range
        } else {
            textBox.SetActive(false);
        }
    }

    void OnTriggerExit(Collider col) {
        inRange = false; // update in range when leaving lever tentacle trigger range
        textBox.SetActive(false);
    }

    void UpdateDistCovered(int dist_) { DistCovered = dist_; }
}
