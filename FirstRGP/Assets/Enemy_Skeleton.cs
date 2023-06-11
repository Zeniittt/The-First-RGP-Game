using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    [Header("Move Infor")]
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(!isGrounded || isWallDetected)
        {
            FlipFacing();
        }

        rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
    }
}
