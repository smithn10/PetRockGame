using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTentacleSwitch : MonoBehaviour {
    public GameObject cam;
    public GameObject target;
    public GameObject activeState;
    public GameObject rock;
    public int ticks;
    public int i = 0;
    public float speed = 0f;
    public float angleThreshold;
    public bool leverActive;
    private bool inRange = false;

    void Update() {
        if (inRange) {
            Vector3 direction = (transform.position - rock.transform.position).normalized;
            float angle = Vector3.Dot(direction, rock.transform.forward);
            float distance = direction.magnitude;

            if((angle > angleThreshold) && (Input.GetKeyDown(KeyCode.E))) {
                activeState.SetActive(!activeState.activeSelf);
                activeState.SendMessage("UpdateI", i);
                gameObject.SetActive(!gameObject.activeSelf);
            }
        }

        if ((leverActive) && (i < ticks)) {
            target.transform.Translate(Vector3.right * speed * Time.deltaTime);
            i++;
        } else if ((!leverActive) && (i > 0)) {
            target.transform.Translate(Vector3.left * speed * Time.deltaTime);
            i--;
        }
    }


    void OnTriggerEnter(Collider col) {
        if (col.name == "Rock") {
            inRange = true; // update in range when entering lever tentacle trigger range
        }
    }

    void OnTriggerExit(Collider col) {
        inRange = false; // update in range when leaving lever tentacle trigger range
    }

    void UpdateI(int i_) { i = i_; }
}
