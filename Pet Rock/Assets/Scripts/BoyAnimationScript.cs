using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyAnimationScript : MonoBehaviour {
    public Animator playerAnimator;
    public Animator stage2Animator;

    void Climbing(bool b) { if(playerAnimator != null) playerAnimator.SetBool("isClimbing", b); }
    void Holding(bool b) { if (playerAnimator != null) playerAnimator.SetBool("isHolding", b); }
    void Falling(bool b) { if (playerAnimator != null) playerAnimator.SetBool("isFalling", b); }
    void Landing(bool b) { if (playerAnimator != null) playerAnimator.SetBool("isLanding", b); }
    void Jumping(bool b) { if (playerAnimator != null) if (playerAnimator != null) playerAnimator.SetBool("isJumping", b); }
    void Gliding(bool b) { playerAnimator.SetBool("isGliding", b); }
    void SetAnimSpeed(float s) { if (playerAnimator != null) playerAnimator.SetFloat("Speed", s); }
    void PauseAnim(bool b) {
        if(b) { if(playerAnimator != null)playerAnimator.enabled = false; }
        else {
            playerAnimator.enabled = true;
        }
    }
    public void Evolve()
    {
        playerAnimator = stage2Animator;
    }
}

