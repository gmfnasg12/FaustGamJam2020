using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour {
 
    //Public Variables\\
    public float jumpForce = 0.0f;
    public Transform isGroundedChecker; 
    public float checkGroundRadius = 0.5f; 
    public LayerMask groundLayer;
    public GameObject playerSprite = null;
    public float speed = 1;
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

        // Change direction
        ChangeDirection();

    }
    void ChangeDirection()
    {
        bool moveToRight = Input.GetAxis ("Horizontal") > 0;
        bool moveToLeft = Input.GetAxis ("Horizontal") < 0;
        SpriteRenderer SR = playerSprite.GetComponent<SpriteRenderer>();

        if (moveToRight && faceRight != true)
        {
            faceRight = true;
            SR.flipX = false;
        } 
        else if (moveToLeft && faceRight == true)
        {
            faceRight = false;
            SR.flipX = true;
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
