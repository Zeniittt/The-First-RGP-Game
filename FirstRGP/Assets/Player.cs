using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Infor")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash Infor")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;

    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("Attack Infor")]
    [SerializeField] private float comboTime;
    private float comboTimeCounter;
    private bool isAttacking;
    private int comboCounter;

    private float xInput;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


        Movement();

        CheckInput();


        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeCounter -= Time.deltaTime;
        

        FlipController();

        AnimatorControllers();
    }

    private void Movement()
    {
        if(isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if(dashTime > 0)
        {
            rb.velocity = new Vector2(facingDirection * dashSpeed, 0);
        } else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        //Dòng code ngay bên d??i là gi?i thích d? hi?u nh?t cho dòng code ngay phía trên
        /*if(rb.velocity.x != 0)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }*/

        anim.SetInteger("comboCounter", comboCounter);

        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
    }

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            FlipFacing();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            FlipFacing();
        }
    }

    private void DashAbility()
    {
        if(dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if(comboCounter > 2)
        {
            comboCounter = 0;
        }
    }

    private void StartAttackEvent()
    {
        if(!isGrounded)
        {
            return;
        }
        if (comboTimeCounter < 0)
        {
            comboCounter = 0;
        }
        isAttacking = true;
        comboTimeCounter = comboTime;
    }
}
