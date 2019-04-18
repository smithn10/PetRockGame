using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScript : MonoBehaviour {
    public Animator playerAnim;
    public Animator rockAnim;
    public GameObject player;
    public GameObject rock;
    public GameObject rockMesh;
    public GameObject textBox;
    public Text textInBox;
    public string[] texts;
    public float cooldown = 100;
    private float playerPrevZ;
    private float rockPrevZ;
    private float rockMeshPrevZ;
    private bool first = true;
    private int i = 0;

    void Start() {
        playerPrevZ = player.transform.position.z;
        rockPrevZ = rock.transform.position.z;
        rockMeshPrevZ = rockMesh.transform.position.z;
    }

    void Update() {
        if(player.transform.position.z != playerPrevZ) {
            if(first) { PlayerWalking(true); first = false; }
            else { PlayerRunning(true); }
        } else {
            PlayerWalking(false);
            PlayerRunning(false);
        }

        if(rock.transform.position.z != rockPrevZ || rockMesh.transform.position.z != rockMeshPrevZ) {
            RockMoving(true);
        } else {
            RockMoving(false);
        }

        playerPrevZ = player.transform.position.z;
        rockPrevZ = rock.transform.position.z;
        rockMeshPrevZ = rockMesh.transform.position.z;
    }

    void PlayerWalking(bool b) { playerAnim.SetBool("isWalking", b); }
    void PlayerRunning(bool b) { playerAnim.SetBool("isRunning", b); }
    void RockMoving(bool b) { rockAnim.SetBool("isMoving", b); }
    public void RockBite(bool b) { rockAnim.SetBool("isBiting", b); }

    void ShowText(int i) {
        if (i >= texts.Length) { textInBox.text = ""; }
        else { textInBox.text = texts[i]; }
    }
}
