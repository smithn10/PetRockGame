using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndGateOpener : MonoBehaviour {
    public GameObject player;
    public GameObject rock;
    public GameObject endGate;
    public TextAsset[] files;
    public int stage = 0;
    public int enemyCount;
    private bool opened = false;
    private DialogueManager dialogueManager;
    
    void Start() {
        dialogueManager = GetComponent<DialogueManager>(); // get dialogue manager
    }

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
                    GiveNewTip(files[0]);
                    break;
                case 1:
                    rock.GetComponent<CharControl>().glideEnabled = true;
                    rock.transform.GetChild(1).gameObject.SetActive(false);
                    rock.transform.GetChild(2).gameObject.SetActive(true);
                    rock.GetComponent<BoyAnimationScript>().Evolve();
                    GiveNewTip(files[1]);
                    break;
                case 2:
                    rock.transform.GetChild(2).gameObject.SetActive(false);
                    rock.transform.GetChild(3).gameObject.SetActive(true);
                    rock.GetComponent<BoyAnimationScript>().Evolve();
                    GiveNewTip(files[2]);
                    break;
                case 3:
                    rock.transform.GetChild(3).gameObject.SetActive(false);
                    rock.transform.GetChild(4).gameObject.SetActive(true);
                    rock.GetComponent<BoyAnimationScript>().Evolve();
                    GiveNewTip(files[3]);
                    break;
            }
        }
    }

    void DecreaseCount() { enemyCount--; }

    void GiveNewTip(TextAsset file) {
        dialogueManager.Reload(file); // reload function described in DialogueManager script
        dialogueManager.currLineIndex = 0;
        dialogueManager.EnableTextBox();
    }
}
