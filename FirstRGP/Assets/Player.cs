using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private bool isMoving;
    private float xInput;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


        isMoving = rb.velocity.x != 0;
        //D�ng code ngay b�n d??i l� gi?i th�ch d? hi?u nh?t cho d�ng code ngay ph�a tr�n
        /*if(rb.velocity.x != 0)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }*/
        anim.SetBool("isMoving", isMoving);
    }
}
