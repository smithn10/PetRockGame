using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyAnimationScript : MonoBehaviour {
    public Animator playerAnimator;

    void Climbing(bool b) { playerAnimator.SetBool("isClimbing", b); }
    void Running(bool b) { playerAnimator.SetBool("isRunning", b); }
    void Holding(bool b) { playerAnimator.SetBool("isHolding", b); }
    void Walking(bool b) { playerAnimator.SetBool("isWalking", b); }
    void Falling(bool b) { playerAnimator.SetBool("isFalling", b); }
    void Landing(bool b) { playerAnimator.SetBool("isLanding", b); }
    void Jumping(bool b) { playerAnimator.SetBool("isJumping", b); }
    void PauseAnim(bool b) {
        if(b) { playerAnimator.enabled = false; }
        else {
            playerAnimator.enabled = true;
        }
    }
}

