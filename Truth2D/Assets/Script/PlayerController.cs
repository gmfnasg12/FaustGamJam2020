﻿using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour {
 
    //Public Variables\\
    public float jumpForce = 0.0f;
    public Transform isGroundedChecker; 
    public float checkGroundRadius = 0.5f; 
    public LayerMask groundLayer;
    public Transform playerTransform = null;
    public float speed = 1;
    public Animator playerMovement = null;
    private bool faceRight = true;
    
    #region ground checker
    [SerializeField]
    bool isGrounded = false; 

    #endregion
 
    //Private Variables\\
 
    private Rigidbody rb;
 
    //Initiate at first frame of game\\
 
    void Start ()
    {
        //Calling Components\\
 
        rb = GetComponent<Rigidbody> ();
    }
 
    //Initiate at a set time\\
 
    void Update() 
    { 
        Move(); 
        CheckIfGrounded();
        Jump();
    } 
 
    //Pick-up Collision\\
    
    void Move() 
    { 
        // float x = Input.GetAxisRaw("Horizontal"); 
        // float moveBy = x * speed; 
        // rb.velocity = new Vector2(moveBy, rb.velocity.y); 
        
        transform.position += Vector3.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
        Animation();
        // Change direction
        ChangeDirection();

    }
    void Animation()
    {
        playerMovement.SetBool("isWalking", Input.GetAxis ("Horizontal") != 0);
    }
    void ChangeDirection()
    {
        bool moveToRight = Input.GetAxis ("Horizontal") > 0;
        bool moveToLeft = Input.GetAxis ("Horizontal") < 0;

        if (moveToRight && faceRight != true)
        {
            faceRight = true;
            playerTransform.localScale = new Vector3(- playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        } 
        else if (moveToLeft && faceRight == true)
        {
            faceRight = false;
            playerTransform.localScale = new Vector3(- playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        }
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            other.gameObject.SetActive (false);
        }
    }
    void CheckIfGrounded() 
    { 
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer); 
        if (collider != null) 
            isGrounded = true; 
        else 
            isGrounded = false; 
    }   
    void Jump() 
    { 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
    }

}
