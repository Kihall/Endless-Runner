using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private Animator animator;
    private HealthSystem healthSystem;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        healthSystem = playerController.GetComponent<HealthSystem>();

        healthSystem.OnDead += HealthSystem_OnDead;
    }

    void Update()
    {
        if (healthSystem.IsDead()) return;

        JumpingAnimation();
        RunningAnimation();
        FlipSpriteOnAir();
    }

    private void RunningAnimation()
    {
        if (playerController.IsGrounded())
        {
            animator.SetBool("onGround", true);
            if (playerController.GetRigidbody2D().velocity.x == 0)
            {
                animator.SetBool("run", false);
            }
            if (playerController.GetRigidbody2D().velocity.x > 0 && playerController.GetMoveDir().x > 0)
            {
                animator.SetBool("run", true);
                playerController.transform.localScale = new Vector3(1, 1, 1);
            }
            if (playerController.GetRigidbody2D().velocity.x < 0 && playerController.GetMoveDir().x < 0)
            {
                animator.SetBool("run", true);
                playerController.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void JumpingAnimation()
    {
        if (!playerController.IsGrounded())
        {
            animator.SetFloat("verticalVelocity", playerController.GetRigidbody2D().velocity.y);
            if (playerController.GetRigidbody2D().velocity.y < -0.1f)
            {
                animator.SetBool("onGround", false);
            }
        }
    }

    private void FlipSpriteOnAir()
    {
        if (playerController.GetRigidbody2D().velocity.x > 0)
        {
            playerController.transform.localScale = new Vector3(1, 1, 1);
        }
        if (playerController.GetRigidbody2D().velocity.x < 0)
        {
            playerController.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        animator.SetTrigger("dead");
    }
}
