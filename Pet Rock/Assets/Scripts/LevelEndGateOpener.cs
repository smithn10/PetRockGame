using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndGateOpener : MonoBehaviour {
    public GameObject player;
    public GameObject rock;
    public GameObject endGate;
    public int stage = 0;
    public int enemyCount;
    private bool opened = false;

    // Update is called once per frame
    void Update() {
        if(enemyCount <= 0 && !opened) {
            opened = true;
            endGate.GetComponent<Collider>().enabled = false;
            endGate.GetComponent<MeshRenderer>().enabled = false;

            switch (stage)
            {
                case 0:
                    transform.GetComponent<GameManager>().canChangeChar = true;
                    rock.transform.GetChild(0).gameObject.SetActive(false);
                    rock.transform.GetChild(1).gameObject.SetActive(true);
                    rock.GetComponent<BoyAnimationScript>().Evolve();
                    break;
                case 1:
                    rock.GetComponent<CharControl>().glideEnabled = true;
                    rock.transform.GetChild(1).gameObject.SetActive(false);
                    rock.transform.GetChild(2).gameObject.SetActive(true);
                    rock.GetComponent<BoyAnimationScript>().Evolve();
                    break;
                case 2:
                    rock.transform.GetChild(2).gameObject.SetActive(false);
                    rock.transform.GetChild(3).gameObject.SetActive(true);
                    rock.GetComponent<BoyAnimationScript>().Evolve();
                    break;
                case 3:
                    rock.transform.GetChild(3).gameObject.SetActive(false);
                    rock.transform.GetChild(4).gameObject.SetActive(true);
                    rock.GetComponent<BoyAnimationScript>().Evolve();
                    break;
            }
        }
    }

    void DecreaseCount() { enemyCount--; }
}
