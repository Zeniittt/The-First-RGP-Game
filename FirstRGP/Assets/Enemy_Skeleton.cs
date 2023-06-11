using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    bool isAttacking;

    [Header("Move Infor")]
    [SerializeField] private float moveSpeed;

    [Header("Player detection")]
    [SerializeField] private float checkDistancePlayer;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(isPlayerDetected)
        {
            if(isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
                Debug.Log("I see the dbrr");
                isAttacking = false;
            } else
            {
                Debug.Log("Attack!!!!!!" + isPlayerDetected.collider.gameObject.name);
                isAttacking = true;
            }
        }

        if(!isGrounded || isWallDetected)
        {
            FlipFacing();
        }

        Movement();
    }

    private void Movement()
    {
        if(!isAttacking)
        {
            rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
        }
    }

    protected override void CheckCollision()
    {
        base.CheckCollision();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, checkDistancePlayer * facingDirection, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + checkDistancePlayer * facingDirection, transform.position.y));
    }
}
