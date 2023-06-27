using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private LayerMask groundLayerMask;

    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private HealthSystem healthSystem;

    private Vector3 moveDir;
    private bool stopTime;
    private float stopTimeTimer = 3f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        healthSystem.OnDead += Player_OnDead;
    }

    private void Update()
    {
        if (stopTime)
        {
            stopTimeTimer -= Time.deltaTime;
            if (stopTimeTimer < 0)
            {
                Time.timeScale = 0;
            }
        }

        if (healthSystem.IsDead()) return;

        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
            moveDir = new Vector3(-moveSpeed, 0, 0).normalized;
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody2D.velocity = new Vector2(+moveSpeed, rigidbody2D.velocity.y);
                moveDir = new Vector3(+moveSpeed, 0, 0).normalized;
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidbody2D.velocity = Vector2.up * jumpVelocity;
        }
    }

    private void Player_OnDead(object sender, EventArgs e)
    {
        stopTime = true;
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(
            boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayerMask);
        return raycastHit2D.collider != null;
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rigidbody2D;
    }

    public Vector3 GetMoveDir()
    {
        return moveDir;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
